using Car_Rental_System_New.Model;

namespace Car_Rental_System_New.Repositories
{
    public interface IPaymentService
    {
        Payment MakePayment(int reservationId, decimal amount);
        Payment IssueRefund(int paymentId, decimal refundAmount);
        Payment ConfirmPayment(int paymentId);
    }
}
