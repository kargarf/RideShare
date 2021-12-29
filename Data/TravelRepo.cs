using RideShare.Models;

namespace RideShare.Data
{
    public class TravelRepo : ITravelRepo
    {
        private readonly AppDbContext _context;

        public TravelRepo(AppDbContext context)
        {
            _context = context;
        }
        public void CreateTravel(Travel travel)
        {
            if (travel == null)
            {
                throw new ArgumentNullException(nameof(travel));
            }
            _context.Travels.Add(travel);
        }

        public IEnumerable<Travel> GetAllTravels()
        {
            return _context.Travels.ToList();
        }

        public User GetUserById(int id)
        {
            return _context.Users.FirstOrDefault(x => x.Id == id);
        }

        public Travel GetTravelById(int id)
        {
            return _context.Travels.FirstOrDefault(t => t.Id == id);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public IEnumerable<Trip> GetAllTrips()
        {
            return _context.Trips.ToList();
        }
    }
}