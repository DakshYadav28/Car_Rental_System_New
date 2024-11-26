using Car_Rental_System_New.Model;
using Car_Rental_System_New.Models;
using Microsoft.EntityFrameworkCore;

namespace Car_Rental_System_New.Repositories
{
    public class PaymentService : IPaymentService
    {
        private readonly MyContext _context;

        public PaymentService(MyContext context)
        {
            _context = context;
        }

        public Payment MakePayment(int reservationId, decimal amount)
        {
            var payment = new Payment
            {
                ReservationId = reservationId,
                Amount = amount,
                PaymentDate = DateTime.UtcNow,
                PaymentStatus = "Pending"
            };

            _context.Payment.Add(payment);
            _context.SaveChanges();
            return payment;
        }

        public Payment ConfirmPayment(int paymentId)
        {
            var payment = _context.Payment
                .Include(p => p.Reservation)
                .FirstOrDefault(p => p.ReservationId == paymentId);

            if (payment == null)
                throw new ArgumentException("Invalid Payment ID.");


            payment.PaymentStatus = "Completed";


            if (payment.Reservation != null)
                payment.Reservation.Status = "Confirmed";

            _context.SaveChanges();
            return payment;
        }

        public Payment IssueRefund(int paymentId, decimal refundAmount)
        {
            var payment = _context.Payment
                .Include(p => p.Reservation)
                .FirstOrDefault(p => p.ReservationId == paymentId);

            if (payment == null)
                throw new ArgumentException("Invalid Payment ID.");

            if (payment.PaymentStatus != "Completed")
                throw new InvalidOperationException("Refunds can only be issued for completed payments.");

            payment.Amount = refundAmount;
            payment.PaymentStatus = "Refunded";

            _context.SaveChanges();
            return payment;
        }


    }
}
