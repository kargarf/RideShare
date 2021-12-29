using System.ComponentModel.DataAnnotations;

namespace RideShare.Models
{
    public class Trip
    {
        public int TravelId { get; set; }
        public Travel Travel { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}