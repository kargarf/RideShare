using System.ComponentModel.DataAnnotations;

namespace RideShare.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string Surname { get; set; }
        public IList<Trip> Trips { get; set; }
    }
    
}