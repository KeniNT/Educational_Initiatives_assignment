using System;
using SmartOffice.Models;
using SmartOffice.Infrastructure;

namespace SmartOffice.Services
{
    // Mock notification service. Replace with real integration if required.
    public sealed class NotificationService
    {
        private static readonly Lazy<NotificationService> _lazy = new(() => new NotificationService());
        public static NotificationService Instance => _lazy.Value;

        private NotificationService() { }

        public void NotifyBookingReleased(Booking booking)
        {
            // For this exercise we just log & console message
            var msg = $"[Notification] Booking {booking.Id} for room {booking.RoomId} was released automatically at {DateTime.Now:HH:mm}.";
            Logger.Info(msg);
            Console.WriteLine(msg);
        }
    }
}
