using Car_Rental_System_New.Models;

namespace Car_Rental_System_New.Repositories
{
    public interface IUserService
    {
        User Register(string username, string password, string role, string email);
        string Authenticate(string username, string password);
    }
}
