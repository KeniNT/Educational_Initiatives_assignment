using System;
using SmartOffice.Infrastructure;
using SmartOffice.Models;

namespace SmartOffice.Observers
{
    public class AirConditioner : IOccupancyObserver
    {
        public void OnOccupancyChanged(Room room)
        {
            if (room.IsOccupied)
            {
                Logger.Info($"AC ON for Room {room.Id}");
                Console.WriteLine($"Room {room.Id}: AC turned on.");
            }
            else if (room.OccupantCount == 0)
            {
                Logger.Info($"AC OFF for Room {room.Id}");
                Console.WriteLine($"Room {room.Id}: AC turned off.");
            }
        }
    }
}
