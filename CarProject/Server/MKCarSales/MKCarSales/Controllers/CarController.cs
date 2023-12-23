using Microsoft.AspNetCore.Mvc;
using MKCarSales.Interfaces;
using MKCarSales.Models.DTOs;

namespace MKCarSales.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CarController : ControllerBase
{
    private readonly ICarService _carService;
    public CarController(ICarService carService)
    {
        _carService = carService;
    }

    [HttpGet("{id}", Name = "GetCar")]
    public async Task<ActionResult<CarDto>> GetCarAsync(Guid id)
    {
        var carDto = await _carService.GetCarByIdAsync(id);

        return carDto is null
            ? NotFound()
            : Ok(carDto);
    }

    [HttpPost]
    public async Task<IActionResult> PostCar([FromForm] CreateCarDto createCarDto)
    {
        var createdCar = await _carService.AddCarAsync(createCarDto);
        return CreatedAtAction("GetCar", new { id = createdCar.Id }, null);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCar(Guid id)
    {
        await _carService.DeleteCarAsyncById(id);

        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutCar(Guid id,[FromForm] UpdateCarDto car)
    {
        //if (!ModelState.IsValid)
        //{
        //    return BadRequest(ModelState);
        //}
        if (id == Guid.Empty || id == null)
        {
            throw new ArgumentNullException(nameof(id));
        }
        try
        {
           await _carService.UpdateCarAsync(id, car);
        }
        catch
        {
            return BadRequest();
        }

        return Ok();
    }
    [HttpPost]
    [Route("/api/filter")]
    public async Task<ActionResult<IEnumerable<CarDto>>> GetFilterCar(FilterCarDto filterCarDto)
    {
        var cars = await _carService.GetFilterCar(filterCarDto);
        return Ok(cars);
    }
        
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CarDto>>> GetAllAsync()
    {
        var cars = await _carService.GetAllCarsAsync();

        return Ok(cars);
    }
}