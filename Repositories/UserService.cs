using Car_Rental_System_New.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Car_Rental_System_New.Authentication;

namespace Car_Rental_System_New.Repositories
{
    public class UserService : IUserService
    {
        private readonly MyContext _context;
        private readonly JwtTokenGenerator _jwtTokenGenerator;
        private readonly PasswordHasher<User> _passwordHasher;

        public UserService(MyContext context, IOptions<JwtSettings> jwtSettings, JwtTokenGenerator jwtTokenGenerator)
        {
            _context = context;
            _jwtTokenGenerator = jwtTokenGenerator;
            _passwordHasher = new PasswordHasher<User>();
        }

        public User Register(string username, string password, string role, string email)
        {

            var existingUser = _context.User.SingleOrDefault(u => u.UserName == username);
            if (existingUser != null)
            {
                return null;
            }

            var user = new User
            {
                UserName = username,
                PasswordHash = _passwordHasher.HashPassword(null, password),
                Role = role,
                Email = email
            };

            _context.User.Add(user);
            _context.SaveChanges();
            return user;
        }

        public string Authenticate(string username, string password)
        {
            var user = _context.User.SingleOrDefault(u => u.UserName == username);
            if (user == null) return null;


            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
            if (result == PasswordVerificationResult.Failed)
                return null;


            return _jwtTokenGenerator.GenerateToken(user);
        }
    }
}
