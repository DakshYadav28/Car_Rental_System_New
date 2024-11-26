using Car_Rental_System_New.Model;

namespace Car_Rental_System_New.Models
{
    public class Reservation
    {
        public Reservation()
        {
            Payments = new HashSet<Payment>();
        }

        public int ReservationId { get; set; }
        public int UserId { get; set; }
        public int CarId { get; set; }
        public DateTime PickupDate { get; set; }
        public DateTime DropoffDate { get; set; }
        public string Status { get; set; }
        public decimal TotalPrice { get; set; }

        public virtual Car? Car { get; set; }
        public virtual User? User { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
