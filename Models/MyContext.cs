using Car_Rental_System_New.Model;
using Microsoft.EntityFrameworkCore;

namespace Car_Rental_System_New.Models
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options) { }

        public DbSet<Car> Car { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<Reservation> Reservation { get; set; }
        public DbSet<User> User { get; set; }


    }

}
