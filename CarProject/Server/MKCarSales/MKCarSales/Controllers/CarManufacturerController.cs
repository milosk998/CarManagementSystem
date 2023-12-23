using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MKCarSales.Interfaces;
using MKCarSales.Models.DTOs;
using MKCarSales.Services;

namespace MKCarSales.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CarManufacturerController : ControllerBase
{
    private readonly ICarManufacturerService _carManufacturerService;

    public CarManufacturerController(ICarManufacturerService carManufacturerService)
    {
        _carManufacturerService = carManufacturerService;
    }

    [HttpGet("{id}", Name = "GetCarManufacturer")]
    public async Task<ActionResult<CarDto>> GetCarManufacturerAsync(Guid id)
    {
        var carManufacturerDto = await _carManufacturerService.GetCarManufacturerByIdAsync(id);

        return carManufacturerDto is null
            ? NotFound()
            : Ok(carManufacturerDto);
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CarManufacturerDto>>> GetAllAsync()
    {
        var carManufacturers = await _carManufacturerService.GetAllCarManufacturersAsync();

        return Ok(carManufacturers);
    }

    [HttpPost]
    public async Task<IActionResult> PostCar(CreateCarManufacturerDto createCarManufacturerDto)
    {
        var createdCarManufacturer = await _carManufacturerService.AddAsync(createCarManufacturerDto);
        return CreatedAtAction("GetCarManufacturer", new { id = createdCarManufacturer.Id }, null);
    }
}
