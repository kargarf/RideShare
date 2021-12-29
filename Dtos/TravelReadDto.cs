namespace RideShare.Dtos
{
    public class TravelReadDto
    {
        public int Id { get; set; }
        public string From { get; set; }

        public string To { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }

        public int SeatNumber { get; set; }

        public int UserId { get; set; }

        public bool Active { get; set; }
    }
}