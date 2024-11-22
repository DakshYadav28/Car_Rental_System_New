using Car_Rental_System_New.DTOs;
using Car_Rental_System_New.Models;
using Microsoft.EntityFrameworkCore;

namespace Car_Rental_System_New.Repositories
{
    public class ReservationService : IReservationService
    {
        private readonly MyContext _context;

        public ReservationService(MyContext context)
        {
            _context = context;
        }

        public ReservationDTO BookCar(ReservationCreateDTO reservationDTO)
        {
            var user = _context.User.Find(reservationDTO.UserId);

            if (user == null)
                throw new ArgumentException("Invalid UserId.");

            decimal totalFare = reservationDTO.TotalAmount;

            var reservation = new Reservation
            {
                UserId = reservationDTO.UserId,
                PickupDate = DateTime.UtcNow,
                DropoffDate = DateTime.UtcNow,
                Status = "Confirmed",
                TotalPrice = totalFare
            };

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {

                    if (!ProcessPayment(reservationDTO.UserId, reservationDTO.TotalAmount))
                    {
                        _context.Reservation.Remove(reservation);
                        _context.SaveChanges();

                        transaction.Rollback();
                        throw new InvalidOperationException("Payment processing failed.");
                    }

                    transaction.Commit();

                    return new ReservationDTO
                    {
                        Id = reservation.ReservationId,
                        UserId = (int)reservation.UserId,
                        Username = user.UserName,
                        ReservationDate = reservation.PickupDate,
                        TotalPrice = reservation.TotalPrice,
                        Status = reservation.Status
                    };
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new InvalidOperationException("An error occurred during the reservation process.", ex);
                }
            }
        }

        public IEnumerable<ReservationDTO> GetReservationsByUser(int userId)
        {
            var reservations = _context.Reservation
                .Where(b => b.UserId == userId)
                .ToList();

            return reservations.Select(b => new ReservationDTO
            {
                Id = b.ReservationId,
                UserId = (int)b.UserId,
                Username = b.User.UserName,
                ReservationDate = b.PickupDate,
                TotalPrice = b.TotalPrice,
                Status = b.Status
            }).ToList();
        }
        public IEnumerable<ReservationDTO> GetAllReservations()
        {
            var reservations = _context.Reservation
                .Include(b => b.User)
                .ToList();

            return reservations.Select(b => new ReservationDTO
            {
                Id = b.ReservationId,
                UserId = (int)b.UserId,
                Username = b.User.UserName,  // Show the username for Admin/FlightOwner
                ReservationDate = b.PickupDate,
                TotalPrice = b.TotalPrice,
                Status = b.Status
            }).ToList();
        }
        public ReservationDTO GetReservationById(int reservationId)
        {
            var reservation = _context.Reservation
                .Where(b => b.UserId == reservationId)
                .Include(b => b.User)
                .FirstOrDefault();

            if (reservation == null)
            {
                return null;
            }

            return new ReservationDTO
            {
                Id = reservation.ReservationId,
                UserId = (int)reservation.UserId,
                Username = reservation.User?.UserName,
                TotalPrice = reservation.TotalPrice,
                Status = reservation.Status
            };
        }

        public bool CancelReservation(int reservationId)
        {
            var reservation = _context.Reservation.Find(reservationId);

            if (reservation == null || reservation.Status != "Confirmed")
                throw new InvalidOperationException(" not found or already canceled.");

            reservation.Status = "Canceled";
            _context.SaveChanges();
            return true;
        }

        // Process payment (dummy implementation)
        private bool ProcessPayment(int userId, decimal amount)
        {
            return true;
        }

        // Refund payment (dummy implementation)
        private void RefundPayment(int userId, decimal amount)
        {
            //return true;
        }
    }
}