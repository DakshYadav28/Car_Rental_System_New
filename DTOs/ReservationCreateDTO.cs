namespace Car_Rental_System_New.DTOs
{
    public class ReservationCreateDTO
    {
        public int UserId { get; set; }
        public decimal TotalAmount { get; set; } // Amount for payment
    }
}
