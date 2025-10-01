using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SmartOffice.Infrastructure;

namespace SmartOffice.Models
{
    // Responsible for bookings and auto-release timers
    public sealed class BookingManager
    {
        private readonly ConcurrentDictionary<Guid, (Booking booking, CancellationTokenSource cts)> _tracking = new();

        private static readonly Lazy<BookingManager> _lazy = new(() => new BookingManager());
        public static BookingManager Instance => _lazy.Value;

        private BookingManager() { }

        public Booking CreateBooking(int roomId, DateTime start, int durationMinutes, Room room)
        {
            var b = new Booking(roomId, start, durationMinutes);
            room.Bookings.Add(b);

            // Start a background monitor for occupation within grace period (only if start <= now)
            var now = DateTime.Now;
            if (start <= now && b.Status == BookingStatus.Active)
            {
                StartAutoReleaseWatcher(b, room);
            }
            else
            {
                // If the booking starts in future, we set a timer to start the watcher when booking start arrives.
                var delay = start - now;
                Task.Delay(delay).ContinueWith(_ =>
                {
                    if (b.Status == BookingStatus.Active)
                        StartAutoReleaseWatcher(b, room);
                });
            }

            Logger.Info($"Created booking: {b}");
            return b;
        }

        private void StartAutoReleaseWatcher(Booking booking, Room room)
        {
            // Cancel previous if exists (shouldn't)
            if (_tracking.ContainsKey(booking.Id)) return;

            var cts = new CancellationTokenSource();
            _tracking[booking.Id] = (booking, cts);

            Task.Run(async () =>
            {
                try
                {
                    int grace = OfficeManager.Instance.BookingReleaseDelayMinutes;
                    Logger.Info($"Watcher: booking {booking.Id} waiting {grace} minute(s) for occupancy in room {room.Id}.");
                    var start = DateTime.Now;
                    var deadline = start.AddMinutes(grace);
                    while (DateTime.Now < deadline && !cts.Token.IsCancellationRequested)
                    {
                        if (room.IsOccupied)
                        {
                            Logger.Info($"Watcher: room {room.Id} became occupied for booking {booking.Id}; watcher ending.");
                            RemoveTracking(booking.Id);
                            return;
                        }
                        await Task.Delay(TimeSpan.FromSeconds(5), cts.Token); // poll interval
                    }

                    if (!room.IsOccupied && booking.Status == BookingStatus.Active)
                    {
                        // Release booking
                        booking.Status = BookingStatus.Released;
                        room.Bookings.Remove(booking);
                        Logger.Info($"Booking {booking.Id} for room {room.Id} released due to non-occupancy.");
                        Console.WriteLine($"Room {room.Id} is now unoccupied. Booking released. AC and lights off.");
                        // Notify (mock)
                        Services.NotificationService.Instance.NotifyBookingReleased(booking);
                    }
                }
                catch (OperationCanceledException) { }
                catch (Exception ex)
                {
                    Logger.Error($"Watcher exception: {ex.Message}");
                }
                finally
                {
                    RemoveTracking(booking.Id);
                }
            }, cts.Token);
        }

        private void RemoveTracking(Guid id)
        {
            if (_tracking.TryRemove(id, out var tuple))
            {
                try { tuple.cts.Cancel(); tuple.cts.Dispose(); } catch { }
            }
        }

        public bool CancelBooking(Guid bookingId, Room room)
        {
            var booking = room.Bookings.FirstOrDefault(b => b.Id == bookingId);
            if (booking == null) return false;
            booking.Status = BookingStatus.Cancelled;
            room.Bookings.Remove(booking);
            RemoveTracking(bookingId);
            Logger.Info($"Booking {bookingId} cancelled.");
            return true;
        }

        public bool TryFindConflictingBooking(Room room, DateTime start, DateTime end, out Booking conflict)
        {
            conflict = room.Bookings.FirstOrDefault(b => b.Status == BookingStatus.Active && b.Overlaps(start, end));
            return conflict != null;
        }

        public Booking? GetActiveBooking(Room room)
        {
            return room.Bookings.FirstOrDefault(b => b.Status == BookingStatus.Active);
        }
    }
}
