using Car_Rental_System_New.Models;

namespace Car_Rental_System_New.Repositories
{
    public interface ICarService
    {
        List<Car> GetAllCars();
        Car GetCarById(int id);
        int AddNewCar(Car department);
        string UpdateCar(Car department);
        string DeleteCar(int id);
    }
}
