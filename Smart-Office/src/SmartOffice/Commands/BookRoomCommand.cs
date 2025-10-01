using System;
using System.Globalization;
using System.Linq;
using SmartOffice.Infrastructure;
using SmartOffice.Models;

namespace SmartOffice.Commands
{
    public class BookRoomCommand : ICommand
    {
        private readonly OfficeManager _office;
        private readonly BookingManager _bookingManager;

        public BookRoomCommand(OfficeManager office, BookingManager bookingManager)
        {
            _office = office;
            _bookingManager = bookingManager;
        }

        public bool Execute(string[] args)
        {
            // block <roomId> <HH:mm> <duration>
            if (args.Length < 4) { Console.WriteLine("Usage: block <roomId> <HH:mm> <durationMinutes>"); return false; }
            if (!int.TryParse(args[1], out var roomId)) { Console.WriteLine("Invalid room number. Please enter a valid room number."); return false; }
            var room = _office.GetRoom(roomId);
            if (room == null) { Console.WriteLine($"Room {roomId} does not exist."); return false; }
            var timeStr = args[2];
            if (!DateTime.TryParseExact(timeStr, "HH:mm", null, DateTimeStyles.None, out var time))
            {
                Console.WriteLine("Invalid time. Please provide time in HH:mm format.");
                return false;
            }
            if (!int.TryParse(args[3], out var duration) || duration <= 0) { Console.WriteLine("Invalid duration. Please enter a valid positive number of minutes."); return false; }

            // Convert time (HH:mm) to today's DateTime (assume same day)
            var now = DateTime.Now;
            var start = new DateTime(now.Year, now.Month, now.Day, time.Hour, time.Minute, 0);

            var end = start.AddMinutes(duration);
            if (_bookingManager.TryFindConflictingBooking(room, start, end, out var conflict))
            {
                Console.WriteLine($"Room {roomId} is already booked during this time. Cannot book.");
                return false;
            }

            var booking = _bookingManager.CreateBooking(roomId, start, duration, room);
            Console.WriteLine($"Room {roomId} booked from {start:HH:mm} for {duration} minutes.");
            return false;
        }
    }
}
