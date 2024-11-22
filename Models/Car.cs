namespace Car_Rental_System_New.Models
{
    public class Car
    {

        public Car()
        {
            Reservation = new HashSet<Reservation>();

        }

        public int CarId { get; set; }
        public string Make { get; set; } = null!;
        public string Model { get; set; } = null!;
        public int? Year { get; set; }
        public string Location { get; set; } = null!;
        public decimal PricePerDay { get; set; }
        public string? AvailabilityStatus { get; set; }

        //Car Image
        public string ImageUrl { get; set; } = null!;

        //Car Specification
        public string Specification { get; set; } = null!;
        public HashSet<Reservation> Reservation { get; }
    }
}
