using MKCarSales.Models;
using MKCarSales.Models.DTOs;

namespace MKCarSales.Interfaces;

public interface ICarManufacturerRepository
{
    Task<CarManufacturer> GetByIdAsync(Guid id);
    Task<CarManufacturer> AddCarManufacturerAsync(CarManufacturer carManufacturerToCreate);
    Task<IEnumerable<CarManufacturer>> GetAllCarManufacturerAsync();
}
