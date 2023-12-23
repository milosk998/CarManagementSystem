using MKCarSales.Models.DTOs;

namespace MKCarSales.Interfaces;

public interface ICarManufacturerService
{
    Task<CarManufacturerDto> GetCarManufacturerByIdAsync(Guid id);
    Task<CarManufacturerDto> AddAsync(CreateCarManufacturerDto createCarDto);
    Task<IEnumerable<CarManufacturerDto>> GetAllCarManufacturersAsync();
}
