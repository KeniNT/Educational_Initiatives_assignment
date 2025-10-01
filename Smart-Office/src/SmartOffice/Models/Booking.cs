using System;

namespace SmartOffice.Models
{
    public enum BookingStatus { Active, Cancelled, Released }

    public sealed class Booking
    {
        public Guid Id { get; } = Guid.NewGuid();
        public int RoomId { get; }
        public DateTime Start { get; } // full DateTime for date+time
        public int DurationMinutes { get; }
        public BookingStatus Status { get; set; } = BookingStatus.Active;

        public Booking(int roomId, DateTime start, int durationMinutes)
        {
            RoomId = roomId;
            Start = start;
            DurationMinutes = durationMinutes;
        }

        public DateTime End => Start.AddMinutes(DurationMinutes);

        public bool Overlaps(DateTime otherStart, DateTime otherEnd)
        {
            return Start < otherEnd && otherStart < End;
        }

        public override string ToString()
        {
            return $"Booking {Id:N} - Room {RoomId} {Start:HH:mm} for {DurationMinutes} min (Status: {Status})";
        }
    }
}
