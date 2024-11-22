using Car_Rental_System_New.DTOs;

namespace Car_Rental_System_New.Repositories
{
    public interface IReservationService
    {
        ReservationDTO BookCar(ReservationCreateDTO reservationDTO);
        IEnumerable<ReservationDTO> GetReservationsByUser(int userId);
        bool CancelReservation(int reservationId);
        ReservationDTO GetReservationById(int reservationId);
        IEnumerable<ReservationDTO> GetAllReservations();
    }
}