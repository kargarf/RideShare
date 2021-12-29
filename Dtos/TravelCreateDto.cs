using System.ComponentModel.DataAnnotations;

namespace RideShare.Dtos
{
    public class TravelCreateDto
    {
        [Required]
        public string From { get; set; }

        [Required]
        public string To { get; set; }

        public string Description { get; set; }

        [Required]
        public int SeatNumber { get; set; }

        [Required]
        public bool Active { get; set; }
    }
}