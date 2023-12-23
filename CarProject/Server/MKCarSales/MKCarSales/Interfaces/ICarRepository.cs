using MKCarSales.Models;
using MKCarSales.Models.DTOs;

namespace MKCarSales.Interfaces;

public interface ICarRepository
{
    Task<IEnumerable<Car>> GetAllCarsAsync();
    Task<Car> GetByIdAsync(Guid id);
    Task<Car> AddCarAsync(Car carToAdd);
    Task DeleteCarAsync (Car car);
    Task<Car> UpdateCarAsync(Car carToUpdate);
    Task<IEnumerable<Car>> GetFilterCars(FilterCarDto filterCarDto);
}
