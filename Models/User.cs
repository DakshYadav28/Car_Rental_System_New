using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace Car_Rental_System_New.Models
{
    //public enum UserRole
    //{
    //    Customer,
    //    Admin,
    //    CarOwner
    //}

    public class User
    {
        public User()
        {
            Reservations = new HashSet<Reservation>();

        }

        public int UserId { get; set; }
        public string UserName { get; set; } = null!;
        //public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? LastLogin { get; set; }
        public string? PaymentDetails { get; set; }

        // Role property to distinguish between Customer, Admin, and CarOwner
        public string? Role { get; set; }

        public virtual ICollection<Reservation>? Reservations { get; set; }
    }
}