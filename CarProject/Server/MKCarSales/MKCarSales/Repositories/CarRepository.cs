using Microsoft.EntityFrameworkCore;
using MKCarSales.Interfaces;
using MKCarSales.Models;
using MKCarSales.Models.DTOs;

namespace MKCarSales.Repositories;

public class CarRepository : ICarRepository
{
    private readonly CarSalesDbContext _context;

    public CarRepository(CarSalesDbContext context)
    {
        _context = context;
    }
    public async Task<Car> AddCarAsync(Car carToCreate)
    {
        var createdCar = await _context.Cars.AddAsync(carToCreate);
        await _context.SaveChangesAsync();

        return createdCar.Entity;
    }

    public async Task<Car> GetByIdAsync(Guid id)
    {
        return await _context.Cars
               .Include(lv => lv.Images)
               .Include(lv => lv.CarManufacturer)
               .AsNoTracking() 
               .FirstOrDefaultAsync(car => car.Id == id);
    }
    public async Task<IEnumerable<Car>> GetFilterCars(FilterCarDto filterCarDto)
    {
        var cars = await _context.Cars
                     .Include(lv => lv.CarManufacturer)
                     .Include(lv => lv.Images)
                     .Where(lv => (lv.CarManufacturer.Id == filterCarDto.CarManufacturerId) &&
                     (lv.Power >= filterCarDto.MinPower) && (lv.Power <= filterCarDto.MaxPower) && 
                     (lv.Price >= filterCarDto.MinPrice) && (lv.Price <= filterCarDto.MaxPrice) && 
                     (lv.ProductionYear >= filterCarDto.MinProductionYear) && (lv.ProductionYear <= filterCarDto.MaxProductionYear) && 
                     (lv.CubicCapacity >= filterCarDto.MinCubicCapacity) && (lv.CubicCapacity <= filterCarDto.MaxCubicCapacity) && 
                     (lv.Mileage >= filterCarDto.MinMileage) && (lv.Mileage <= filterCarDto.MaxMileage))
                     .AsNoTracking()
                     .ToListAsync();

        if (filterCarDto.Transmission != null)
        {
            cars = cars.Where(c => c.Transmission == filterCarDto.Transmission).ToList();
        }
        if(filterCarDto.Fuel != null)
        {
            cars = cars.Where(c => c.Fuel == filterCarDto.Fuel).ToList();
        }
        return cars;
    }
    public async Task<IEnumerable<Car>> GetAllCarsAsync()
    {
        return await _context.Cars
           .Include(lv => lv.Images)
           .Include(lv => lv.CarManufacturer)
           .AsNoTracking() 
           .ToListAsync(); 
    }
    public async Task DeleteCarAsync(Car car)
    {
        var imagesToDelete = await _context.Image.Where(i => i.carId == car.Id).ToListAsync();
        _context.Image.RemoveRange(imagesToDelete);

        var carToDelete = await _context.Cars.FindAsync(car.Id);
        if (carToDelete != null)
        {
            _context.Cars.Remove(carToDelete);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<Car> UpdateCarAsync(Car carToUpdate)
    {
        _context.Entry(carToUpdate).State = EntityState.Modified;

        await _context.SaveChangesAsync();

        return carToUpdate;
    }
}
