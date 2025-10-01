using System;
using SmartOffice.Infrastructure;
using SmartOffice.Models;

namespace SmartOffice.Observers
{
    public class Light : IOccupancyObserver
    {
        public void OnOccupancyChanged(Room room)
        {
            if (room.IsOccupied)
            {
                Logger.Info($"Lights ON for Room {room.Id}");
                Console.WriteLine($"Room {room.Id}: Lights turned on.");
            }
            else if (room.OccupantCount == 0)
            {
                Logger.Info($"Lights OFF for Room {room.Id}");
                Console.WriteLine($"Room {room.Id}: Lights turned off.");
            }
        }
    }
}
