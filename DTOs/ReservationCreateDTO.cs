namespace Car_Rental_System_New.DTOs
{
    public class ReservationCreateDTO
    {
        public int ReservationId { get; set; }
        public int UserId { get; set; }
        public int CarId { get; set; }
        public DateTime PickupDate { get; set; }
        public DateTime DropoffDate { get; set; }
        public string Status { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
