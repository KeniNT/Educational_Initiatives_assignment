using System;
using System.Collections.Generic;
using SmartOffice.Observers;

namespace SmartOffice.Models
{
    public sealed class Room
    {
        public int Id { get; }
        public string Name { get; }
        public int MaxCapacity { get; set; } = 10;
        public int OccupantCount { get; private set; } = 0;

        // Observers (AC, Light, sensors)
        private readonly List<IOccupancyObserver> _observers = new();

        // Bookings (managed by BookingManager)
        public List<Booking> Bookings { get; } = new();

        public Room(int id, string name)
        {
            Id = id;
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public void Attach(IOccupancyObserver observer)
        {
            if (!_observers.Contains(observer)) _observers.Add(observer);
        }

        public void Detach(IOccupancyObserver observer)
        {
            _observers.Remove(observer);
        }

        public void SetOccupants(int count)
        {
            if (count < 0) throw new ArgumentException("Occupant count cannot be negative.");
            OccupantCount = count;
            Notify();
        }

        private void Notify()
        {
            foreach (var obs in _observers)
            {
                try { obs.OnOccupancyChanged(this); } catch { /* observer errors should not break flow */ }
            }
        }

        public bool IsOccupied => OccupantCount >= 2;

        public override string ToString()
        {
            var bookingInfo = Bookings.Count == 0 ? "no bookings" : $"{Bookings.Count} booking(s)";
            return $"Room {Id} ({Name}) - Capacity: {MaxCapacity}, Occupants: {OccupantCount}, {bookingInfo}";
        }
    }
}
