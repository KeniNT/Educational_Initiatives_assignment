using System;
using SmartOffice.Infrastructure;
using SmartOffice.Models;

namespace SmartOffice.Commands
{
    public class AddOccupantCommand : ICommand
    {
        private readonly OfficeManager _office;
        public AddOccupantCommand(OfficeManager office) { _office = office; }

        public bool Execute(string[] args)
        {
            // add occupant <roomId> <count>
            if (args.Length < 4) { Console.WriteLine("Usage: add occupant <roomId> <count>"); return false; }
            if (args[1].ToLowerInvariant() != "occupant") { Console.WriteLine("Usage: add occupant <roomId> <count>"); return false; }
            if (!int.TryParse(args[2], out var roomId)) { Console.WriteLine("Invalid room number. Please enter a valid room number."); return false; }
            if (!int.TryParse(args[3], out var count) || count < 0) { Console.WriteLine("Invalid occupant count. Please enter a valid non-negative integer."); return false; }
            var room = _office.GetRoom(roomId);
            if (room == null) { Console.WriteLine($"Room {roomId} does not exist."); return false; }
            if (count > room.MaxCapacity) { Console.WriteLine($"Cannot set occupants to {count}. Exceeds room capacity {room.MaxCapacity}."); return false; }

            room.SetOccupants(count);

            if (count >= 2)
            {
                Console.WriteLine($"Room {room.Id} is now occupied by {count} persons. AC and lights turned on.");
            }
            else if (count == 0)
            {
                Console.WriteLine($"Room {room.Id} is now unoccupied. AC and lights turned off.");
            }
            else
            {
                Console.WriteLine($"Room {room.Id} occupancy insufficient to mark as occupied.");
            }
            return false;
        }
    }
}
