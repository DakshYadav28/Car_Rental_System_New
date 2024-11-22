namespace Car_Rental_System_New.DTOs
{
    public class RegisterModel
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Role { get; set; } = "User";
        public string Email { get; set; } = null!;
    }
}
