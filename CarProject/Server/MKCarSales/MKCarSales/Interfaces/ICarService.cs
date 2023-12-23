using MKCarSales.Models.DTOs;

namespace MKCarSales.Interfaces;

public interface ICarService
{
    Task<IEnumerable<CarDto>> GetAllCarsAsync();
    Task<CarDto> GetCarByIdAsync(Guid id);
    Task<IEnumerable<CarDto>> GetFilterCar(FilterCarDto filterCarDto);
    Task<CarDto> AddCarAsync(CreateCarDto createCarDto);
    Task DeleteCarAsyncById(Guid id);
    Task<CarDto> UpdateCarAsync(Guid id, UpdateCarDto updateCarDto);
}
