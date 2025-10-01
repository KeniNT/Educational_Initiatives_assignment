using System;
using System.Globalization;
using SmartOffice.Infrastructure;
using SmartOffice.Models;
using SmartOffice.Observers;
using SmartOffice.Services;
using System.Linq;

namespace SmartOffice.Commands
{
    public class CommandProcessor
    {
        private readonly OfficeManager _office = OfficeManager.Instance;
        private readonly BookingManager _bookingManager = BookingManager.Instance;

        public CommandProcessor()
        {
            // default booking release delay = 5 minutes (set lower for quick testing if desired)
            _office.BookingReleaseDelayMinutes = 5;
        }

        public bool Process(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return false;
            var parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            var cmd = parts[0].ToLowerInvariant();

            if (cmd == "exit" || cmd == "quit") return true;
            if (cmd == "help") { ShowHelp(); return false; }

            switch (cmd)
            {
                case "config":
                    return new ConfigCommand(_office).Execute(parts);
                case "list":
                    return new ListRoomsCommand(_office).Execute(parts);
                case "add":
                    return new AddOccupantCommand(_office).Execute(parts);
                case "block":
                case "book":
                    return new BookRoomCommand(_office, _bookingManager).Execute(parts);
                case "cancel":
                    return new CancelBookingCommand(_office, _bookingManager).Execute(parts);
                case "status":
                    return HandleStatus(parts);
                default:
                    Console.WriteLine("Unknown command. Type 'help' for commands.");
                    return false;
            }
        }

        private bool HandleStatus(string[] parts)
        {
            if (parts.Length < 2) { Console.WriteLine("Usage: status <roomId>"); return false; }
            if (!int.TryParse(parts[1], out var roomId)) { Console.WriteLine("Invalid room number. Please enter a valid room number."); return false; }
            var room = _office.GetRoom(roomId);
            if (room == null) { Console.WriteLine($"Room {roomId} does not exist."); return false; }
            Console.WriteLine(room.ToString());
            var active = _bookingManager.GetActiveBooking(room);
            if (active != null) Console.WriteLine($"Active booking: {active}");
            return false;
        }

        private void ShowHelp()
        {
            Console.WriteLine("Commands:");
            Console.WriteLine("  config rooms <count>                        - configure number of rooms");
            Console.WriteLine("  config capacity <roomId> <capacity>         - set room capacity");
            Console.WriteLine("  config release_delay <minutes>              - set auto-release delay (minutes)");
            Console.WriteLine("  add occupant <roomId> <count>               - set occupant count");
            Console.WriteLine("  list rooms                                  - list all rooms");
            Console.WriteLine("  block <roomId> <HH:mm> <durationMinutes>    - book a room (alias: book)");
            Console.WriteLine("  cancel <roomId>                              - cancel active booking for room");
            Console.WriteLine("  status <roomId>                              - show room status + active booking");
            Console.WriteLine("  help                                         - show help");
            Console.WriteLine("  exit                                         - exit the program");
        }
    }
}
