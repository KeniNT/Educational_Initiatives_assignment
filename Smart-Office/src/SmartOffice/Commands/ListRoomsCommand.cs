using System;
using SmartOffice.Models;

namespace SmartOffice.Commands
{
    public class ListRoomsCommand : ICommand
    {
        private readonly OfficeManager _office;
        public ListRoomsCommand(OfficeManager office) { _office = office; }
        public bool Execute(string[] args)
        {
            if (args.Length < 2 || args[1].ToLowerInvariant() != "rooms")
            {
                Console.WriteLine("Usage: list rooms");
                return false;
            }

            foreach (var room in _office.Rooms)
            {
                Console.WriteLine(room.ToString());
            }
            return false;
        }
    }
}
