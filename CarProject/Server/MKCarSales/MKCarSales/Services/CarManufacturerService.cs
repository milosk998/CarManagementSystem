using MKCarSales.Interfaces;
using MKCarSales.Models;
using MKCarSales.Models.DTOs;
using MKCarSales.Repositories;

namespace MKCarSales.Services;

public class CarManufacturerService : ICarManufacturerService
{
    public ICarManufacturerRepository _carManufacturerRepository { get; set; }

    public CarManufacturerService(ICarManufacturerRepository carManufacturerRepository)
    {
        _carManufacturerRepository = carManufacturerRepository;            
    }
    public async Task<CarManufacturerDto> AddAsync(CreateCarManufacturerDto createCarManufacturer)
    {
        if(createCarManufacturer == null)
        {
            throw new Exception("No valid paylod provided");
        }

        var carManufacturer = new CarManufacturer()
        {
            Id = Guid.NewGuid(),
            Name = createCarManufacturer.Name,
        };

        var createdCarManufacturer = await _carManufacturerRepository.AddCarManufacturerAsync(carManufacturer);

        if(createdCarManufacturer == null)
        {
            throw new Exception("Something went wrong with creation of car manufacturer");
        }

        return MapCarManufacturerToCarManufacturerDto(createdCarManufacturer);
    }
    public async Task<IEnumerable<CarManufacturerDto>> GetAllCarManufacturersAsync()
    {
        var carManufacturers = await _carManufacturerRepository.GetAllCarManufacturerAsync();

        if (carManufacturers == null)
        {
            throw new Exception("No Car Manufacturers found in database.");
        }


        return carManufacturers.Select(car => MapCarManufacturerToCarManufacturerDto(car)).ToList();
    }

    public async Task<CarManufacturerDto> GetCarManufacturerByIdAsync(Guid id)
    {
        if (id == Guid.Empty || id == null)
        {
            throw new ArgumentNullException(nameof(id));
        }

        var carManufacturer = await _carManufacturerRepository.GetByIdAsync(id);

        if (carManufacturer == null)
        {
            throw new Exception("CarManufacture not found.");
        }

        return MapCarManufacturerToCarManufacturerDto(carManufacturer);
    }

    private CarManufacturerDto MapCarManufacturerToCarManufacturerDto(CarManufacturer carManufacturer)
    {
        return new CarManufacturerDto()
        {
            Id = carManufacturer.Id,
            Name = carManufacturer.Name,
            //Cars = carManufacturer.Cars
        };
    }
}
