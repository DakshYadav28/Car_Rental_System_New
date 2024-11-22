using Car_Rental_System_New.Models;

namespace Car_Rental_System_New.Model
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public int? ReservationId { get; set; }
        public decimal Amount { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string? PaymentMethod { get; set; }
        public string? PaymentStatus { get; set; }

        public virtual Reservation? Reservation { get; set; }
    }
}
