using Car_Rental_System_New.DTOs;
using Car_Rental_System_New.Models;
using Car_Rental_System_New.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Car_Rental_System_New.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        //[Authorize(Roles = "User,Admin")]
        [HttpPost]
        public IActionResult BookCar([FromBody] ReservationCreateDTO reservationDTO)
        {
            if (reservationDTO == null)
                return BadRequest("Invalid booking data.");

            try
            {
                var reservation = _reservationService.BookCar(reservationDTO);
                return Ok(reservation);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[Authorize(Roles = "User,Admin")]
        [HttpGet("{ReservationId}")]
        public IActionResult GetReservationById(int reservationId)
        {
            var reservation = _reservationService.GetReservationById(reservationId);

            if (reservation == null)
                return NotFound("Booking not found.");

            return Ok(reservation);
        }

        //[Authorize(Roles = "User,Admin")]
        [HttpGet("byuser/")]
        public IActionResult GetReservationsByUser()
        {
            var userId = int.Parse(User.FindFirstValue("userId"));  // Extract userId from JWT token
            var reservations = _reservationService.GetReservationsByUser(userId);
            return Ok(reservations);
        }

        //[Authorize(Roles = "User,Admin")]
        [HttpDelete("{bookingId}")]
        public IActionResult CancelReservation(int reservationId)
        {
            try
            {
                _reservationService.CancelReservation(reservationId);
                return Ok("Booking successfully canceled.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        // GET api/bookings/history 
        [Authorize]
        //[HttpGet("history")]
        public IActionResult GetReservationHistory()
        {
            var userRole = User.FindFirst("userRole")?.Value; // Extract custom role from JWT

            if (userRole == null)
            {
                return Forbid();
            }

            if (userRole == "Admin" || userRole == "CarOwner")
            {

                var reservations = _reservationService.GetAllReservations();
                return Ok(reservations);
            }
            else if (userRole == "User")
            {
                var reservations = _reservationService.GetReservationsByUser(int.Parse(User.FindFirst("userId")?.Value));
                return Ok(reservations);
            }

            return Forbid(); // Return 403 if role is not allowed
        }
    }
}
