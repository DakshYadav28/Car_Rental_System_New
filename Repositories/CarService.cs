using Car_Rental_System_New.Models;

namespace Car_Rental_System_New.Repositories
{
    public class CarService : ICarService
    {
        private MyContext _context;
        public CarService(MyContext context)
        {
            _context = context;
        }

        public List<Car> GetAllCars()
        {

            var cars = _context.Car.ToList();
            if (cars.Count > 0)
            { return cars; }
            else
                return null;
        }

        public int AddNewCar(Car car)
        {
            try
            {
                if (car != null)
                {
                    _context.Car.Add(car);
                    _context.SaveChanges();
                    return car.CarId;
                }
                else return 0;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public string DeleteCar(int id)
        {
            if (id != null)
            {
                var car = _context.Car.FirstOrDefault(x => x.CarId == id);
                if (car != null)
                {
                    _context.Car.Remove(car);
                    _context.SaveChanges();
                    return "the given Car id " + id + " Removed";
                }
                else
                    return "Something went wrong with deletion";

            }
            return "Id should not be null or zero";
        }

        public Car GetCarById(int id)
        {
            if (id != 0 || id != null)
            {
                var car = _context.Car.FirstOrDefault(x => x.CarId == id);
                if (car != null)
                    return car;
                else
                    return null;
            }
            return null;
        }

        public string UpdateCar(Car car)
        {
            var existingCar = _context.Car.FirstOrDefault(x => x.CarId == car.CarId);
            if (existingCar != null)
            {
                existingCar.Make = car.Make;
                existingCar.Model = car.Model;
                existingCar.Year = car.Year;
                existingCar.Location = car.Location;
                existingCar.PricePerDay = car.PricePerDay;
                existingCar.AvailabilityStatus = car.AvailabilityStatus;
                existingCar.ImageUrl = car.ImageUrl;
                existingCar.Specification = car.Specification;
                _context.Entry(existingCar).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();
                return "Record Updated successfully";
            }
            else
            {
                return "something went wrong while update";
            }
        }
    }
}
