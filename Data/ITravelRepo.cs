using RideShare.Models;

namespace RideShare.Data
{
    public interface ITravelRepo
    {
        bool SaveChanges();
        IEnumerable<Travel> GetAllTravels();
        Travel GetTravelById(int id);
        void CreateTravel(Travel travel);
        User GetUserById(int id);
        IEnumerable<Trip> GetAllTrips();
    }
}