namespace Car_Rental_System_New.DTOs
{
    public class ReservationDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }  // Include simplified user info (username)
        public DateTime ReservationDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; }
    }
}
