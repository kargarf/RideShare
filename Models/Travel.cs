using System.ComponentModel.DataAnnotations;

namespace RideShare.Models
{
    public class Travel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string From { get; set; }

        [Required]
        public string To { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public string Description { get; set; }

        [Required]
        public int SeatNumber { get; set; }
        public int BookedNumber { get; set; }

        [Required]
        public bool Active { get; set; }

        public IList<Trip> Trips { get; set; }
    }
    
}