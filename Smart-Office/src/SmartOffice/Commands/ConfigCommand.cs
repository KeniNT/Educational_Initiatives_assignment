using System;
using SmartOffice.Infrastructure;
using SmartOffice.Models;
using SmartOffice.Observers;

namespace SmartOffice.Commands
{
    public class ConfigCommand : ICommand
    {
        private readonly OfficeManager _office;
        public ConfigCommand(OfficeManager office) { _office = office; }

        public bool Execute(string[] args)
        {
            if (args.Length < 2) { Console.WriteLine("Invalid config command."); return false; }
            var sub = args[1].ToLowerInvariant();

            if (sub == "rooms")
            {
                if (args.Length < 3) { Console.WriteLine("Usage: config rooms <count>"); return false; }
                if (!int.TryParse(args[2], out var count) || count <= 0) { Console.WriteLine("Invalid room count."); return false; }
                _office.ConfigureRooms(count);

                // Attach default observers to each room (Light + AC)
                foreach (var room in _office.Rooms)
                {
                    room.Attach(new Light());
                    room.Attach(new AirConditioner());
                }

                Console.WriteLine($"Office configured with {count} meeting rooms:{string.Join(", ", _office.Rooms.Select(r => r.Name))}.");
                return false;
            }

            if (sub == "capacity")
            {
                if (args.Length < 4) { Console.WriteLine("Usage: config capacity <roomId> <capacity>"); return false; }
                if (!int.TryParse(args[2], out var roomId)) { Console.WriteLine("Invalid room number. Please enter a valid room number."); return false; }
                if (!int.TryParse(args[3], out var cap) || cap <= 0) { Console.WriteLine("Invalid capacity. Please enter a valid positive number."); return false; }
                var room = _office.GetRoom(roomId);
                if (room == null) { Console.WriteLine($"Room {roomId} does not exist."); return false; }
                room.MaxCapacity = cap;
                Console.WriteLine($"Room {room.Id} maximum capacity set to {cap}.");
                return false;
            }

            if (sub == "release_delay")
            {
                if (args.Length < 3) { Console.WriteLine("Usage: config release_delay <minutes>"); return false; }
                if (!int.TryParse(args[2], out var min) || min < 0) { Console.WriteLine("Invalid minutes."); return false; }
                _office.BookingReleaseDelayMinutes = min;
                Console.WriteLine($"Booking release delay set to {min} minute(s).");
                return false;
            }

            Console.WriteLine("Unknown config option.");
            return false;
        }
    }
}
