using SmartOffice.Models;

namespace SmartOffice.Observers
{
    public interface IOccupancyObserver
    {
        void OnOccupancyChanged(Room room);
    }
}
