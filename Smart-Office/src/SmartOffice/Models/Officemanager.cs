using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using SmartOffice.Infrastructure;

namespace SmartOffice.Models
{
    public sealed class OfficeManager
    {
        private static readonly Lazy<OfficeManager> _lazy = new(() => new OfficeManager());
        public static OfficeManager Instance => _lazy.Value;

        private readonly ConcurrentDictionary<int, Room> _rooms = new();

        private OfficeManager() { }

        // booking release delay (minutes)
        public int BookingReleaseDelayMinutes { get; set; } = 5;

        public IReadOnlyCollection<Room> Rooms => _rooms.Values.OrderBy(r => r.Id).ToList().AsReadOnly();

        public void ConfigureRooms(int count)
        {
            if (count <= 0) throw new ArgumentException("Room count must be positive.");
            _rooms.Clear();
            for (int i = 1; i <= count; i++)
            {
                var r = new Room(i, $"Room {i}");
                _rooms[r.Id] = r;
            }
            Logger.Info($"Office configured with {count} rooms.");
        }

        public Room? GetRoom(int id)
        {
            if (id <= 0) return null;
            _rooms.TryGetValue(id, out var r);
            return r;
        }
    }
}
