using Car_Rental_System_New.Models;
using Car_Rental_System_New.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Car_Rental_System_New.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICarService _service;
        public CarController(ICarService service)
        {
            _service = service;
        }

        [HttpGet("GetAllCars")]
        public IActionResult GetAll()
        {
            List<Car> cars = _service.GetAllCars();
            return Ok(cars);
        }

        [HttpGet("{id}")]
        public IActionResult GetCarById(int id)
        {
            Car car = _service.GetCarById(id);
            return Ok(car);
        }

        [HttpPost]
        public IActionResult Post(Car car)
        {
            int Result = _service.AddNewCar(car);
            return Ok(Result);
        }

        [HttpPut]
        public IActionResult Put(Car car)
        {
            string result = _service.UpdateCar(car);
            return Ok(result);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            string result = _service.DeleteCar(id);
            return Ok(result);
        }
    }
}
