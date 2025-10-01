using System;
using System.Linq;
using SmartOffice.Infrastructure;
using SmartOffice.Models;

namespace SmartOffice.Commands
{
    public class CancelBookingCommand : ICommand
    {
        private readonly OfficeManager _office;
        private readonly BookingManager _bookingManager;
        public CancelBookingCommand(OfficeManager office, BookingManager bookingManager)
        {
            _office = office;
            _bookingManager = bookingManager;
        }

        public bool Execute(string[] args)
        {
            // cancel <roomId>  (cancels active booking if any)
            if (args.Length < 2) { Console.WriteLine("Usage: cancel <roomId>"); return false; }
            if (!int.TryParse(args[1], out var roomId)) { Console.WriteLine("Invalid room number. Please enter a valid room number."); return false; }
            var room = _office.GetRoom(roomId);
            if (room == null) { Console.WriteLine($"Room {roomId} does not exist."); return false; }

            var active = _bookingManager.GetActiveBooking(room);
            if (active == null)
            {
                Console.WriteLine($"Room {roomId} is not booked. Cannot cancel booking.");
                return false;
            }

            var result = _bookingManager.CancelBooking(active.Id, room);
            if (result)
            {
                Console.WriteLine($"Booking for Room {roomId} cancelled successfully.");
            }
            else
            {
                Console.WriteLine($"Failed to cancel booking for Room {roomId}.");
            }
            return false;
        }
    }
}
