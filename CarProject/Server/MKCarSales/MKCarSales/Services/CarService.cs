using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS.Core;
using MKCarSales.Interfaces;
using MKCarSales.Models;
using MKCarSales.Models.DTOs;

namespace MKCarSales.Services;

public class CarService : ICarService
{
    private readonly ICarRepository _carRepository;
    private readonly IMediaService _mediaService;
    private readonly ICarManufacturerRepository _carManufacturerRepository;

    public CarService(ICarRepository carRepository, IMediaService mediaService, ICarManufacturerRepository carManufacturerRepository)
    {
        _carRepository = carRepository;
        _mediaService = mediaService;
        _carManufacturerRepository = carManufacturerRepository;
    }
    public async Task<CarDto> GetCarByIdAsync(Guid id)
    {
        if (id == Guid.Empty || id == null)
        {
            throw new ArgumentNullException(nameof(id));
        }

        var car = await _carRepository.GetByIdAsync(id);

        if (car == null)
        {
            throw new Exception("Car not found.");
        }

        return MapCarToCarDto(car);
    }

    public async Task<CarDto> AddCarAsync(CreateCarDto createCarDto)
    {
        var carManufacturer = await _carManufacturerRepository.GetByIdAsync(createCarDto.CarManufacturerId);

        if (carManufacturer == null)
        {
            throw new Exception("Bad request. Car manufacturer provided does not exist");
        }

        var fileUploadTasks = createCarDto.Files?.Select(f => _mediaService.UploadCarImageAsync(f));

        var images = Array.Empty<Models.Image>();

        if (fileUploadTasks is not null)
        {
            images = await Task.WhenAll(fileUploadTasks);
        }

        var carToCreate = new Car()
        {
            Id = Guid.NewGuid(),
            Name = createCarDto.Name,
            Price = createCarDto.Price,
            Description = createCarDto.Description,
            ProductionYear = createCarDto.ProductionYear,
            VehicleType = createCarDto.VehicleType,
            CubicCapacity = createCarDto.CubicCapacity,
            Fuel = createCarDto.Fuel,
            Power = createCarDto.Power,
            Color = createCarDto.Color,
            Mileage = createCarDto.Mileage,
            Transmission = createCarDto.Transmission,
            Images = images,
            CarManufacturerId = carManufacturer.Id,
        };

        var createdCar = await _carRepository.AddCarAsync(carToCreate);

        return new CarDto() 
        {
            Id = createdCar.Id,
            Name = createdCar.Name,
            Price = createdCar.Price,
            Description = createdCar.Description,
            ProductionYear = createdCar.ProductionYear,
            CubicCapacity = createCarDto.CubicCapacity,
            VehicleType = createdCar.VehicleType,
            Fuel = createdCar.Fuel,
            Power = createdCar.Power,
            Color = createdCar.Color,
            Mileage = createdCar.Mileage,
            Transmission = createdCar.Transmission,
            Images = createdCar.Images,
        };
    }

    public async Task<IEnumerable<CarDto>> GetAllCarsAsync()
    {
        var cars = await _carRepository.GetAllCarsAsync();

        if (cars == null)
        {
            throw new Exception("No cars found in database.");
        }

        return cars.Select(car => MapCarToCarDto(car)).ToList();
    }
    public async Task<IEnumerable<CarDto>> GetFilterCar(FilterCarDto filterCarDto)
    {
        CheckNullForNumericPropertiesAndSetDefaultValues(filterCarDto);
        var cars = await _carRepository.GetFilterCars(filterCarDto);

        if(cars == null)
        {
            throw new Exception("No cars found in database.");
        }

        return cars.Select(car => MapCarToCarDto(car)).ToList();
    }

    private void CheckNullForNumericPropertiesAndSetDefaultValues(FilterCarDto filterCarDto)
    {
        if(filterCarDto.MinPower == null)
        {
            filterCarDto.MinPower = 0;
        }
        if(filterCarDto.MaxPower == null)
        {
            filterCarDto.MaxPower = 3000;
        }
        if(filterCarDto.MinPrice == null)
        {
            filterCarDto.MinPrice = 0;
        }
        if(filterCarDto.MaxPrice == null)
        {
            filterCarDto.MaxPrice = 999999;
        }
        if(filterCarDto.MinCubicCapacity == null)
        {
            filterCarDto.MinCubicCapacity = 0;
        }
        if(filterCarDto.MaxCubicCapacity == null)
        {
            filterCarDto.MaxCubicCapacity = 15000;
        }
        if(filterCarDto.MinMileage == null)
        {
            filterCarDto.MinMileage = 0;
        }
        if(filterCarDto.MaxMileage == null){
            filterCarDto.MaxMileage = 9999999;
        }
    }

    private CarDto MapCarToCarDto(Car car)
    {
        var updateCar = new CarDto() 
        {
            Id = car.Id,
            Name = car.Name,
            Price = car.Price,
            Description = car.Description,
            ProductionYear = car.ProductionYear,
            VehicleType = car.VehicleType,
            CubicCapacity = car.CubicCapacity,
            Fuel = car.Fuel,
            Power = car.Power,
            Color = car.Color,
            Mileage = car.Mileage,
            Transmission = car.Transmission,
            Images = car.Images,
            CarManufacturer = new CarManufacturerDto
            {
                Id = car.CarManufacturer.Id ,
                Name = car.CarManufacturer.Name
            }
        };
        return updateCar;
    }
    public async Task DeleteCarAsyncById(Guid id)
    {
        var car = await _carRepository.GetByIdAsync(id);

        if (car == null)
        {
            throw new Exception("Bad request. Car provided does not exist");
        }
        foreach(var image in car.Images)
        {
            await _mediaService.DeleteCarImageAsync(image.Id);
        }

        await _carRepository.DeleteCarAsync(car);

    }

    public async Task<CarDto> UpdateCarAsync(Guid id,UpdateCarDto updateCarDto)
    {
        var updateCar = await _carRepository.GetByIdAsync(id);

        if (updateCar == null)
        {
            throw new Exception("Bad request. Car provided does not exist");
        }

        var carManufacturer = await _carManufacturerRepository.GetByIdAsync(updateCarDto.CarManufacturerId);
        if (carManufacturer == null)
        {
            throw new Exception("Bad request. Car Manufacturer provided does not exist");
        }


        updateCar.Name = updateCarDto.Name;
        updateCar.Price = updateCarDto.Price;
        updateCar.Description = updateCarDto.Description;
        updateCar.ProductionYear = updateCarDto.ProductionYear;
        updateCar.VehicleType = updateCarDto.VehicleType;
        updateCar.Fuel = updateCarDto.Fuel;
        updateCar.CubicCapacity = updateCarDto.CubicCapacity;
        updateCar.Power = updateCarDto.Power;
        updateCar.Color = updateCarDto.Color;
        updateCar.Mileage = updateCarDto.Mileage;
        updateCar.Transmission = updateCarDto.Transmission;
        if (updateCar.CarManufacturerId != updateCarDto.CarManufacturerId)
        {
            updateCar.CarManufacturerId = carManufacturer.Id;
            updateCar.CarManufacturer = carManufacturer;
        }
       

        var car = await _carRepository.UpdateCarAsync(updateCar);

        return MapCarToCarDto(car);

    }
}
