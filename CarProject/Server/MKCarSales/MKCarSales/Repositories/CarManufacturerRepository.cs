using Microsoft.EntityFrameworkCore;
using MKCarSales.Interfaces;
using MKCarSales.Models;
using MKCarSales.Models.DTOs;

namespace MKCarSales.Repositories;

public class CarManufacturerRepository : ICarManufacturerRepository
{

    private readonly CarSalesDbContext _context;

    public CarManufacturerRepository(CarSalesDbContext context)
    {
        _context = context;
    }

    public async Task<CarManufacturer> AddCarManufacturerAsync(CarManufacturer carManufacturerToCreate)
    {
        var createdCarManufacturer = await _context.CarManufacturers.AddAsync(carManufacturerToCreate);
        await _context.SaveChangesAsync();

        return createdCarManufacturer.Entity;
    }
    public async Task<IEnumerable<CarManufacturer>> GetAllCarManufacturerAsync()
    {
        return await _context.CarManufacturers
            .Include(lv => lv.Cars)
            .OrderBy(lv => lv.Name)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<CarManufacturer> GetByIdAsync(Guid id)
    {
        return await _context.CarManufacturers
              .Include(lv => lv.Cars)
              .AsNoTracking()
              .FirstOrDefaultAsync(carManufacturer => carManufacturer.Id == id);
              
    }
}
