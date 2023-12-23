using System.ComponentModel.DataAnnotations;

namespace MKCarSales.Models.DTOs;

public class CreateCarDto
{
    //[Required]
    public string Name { get; set; }
    //[Range(0, 999999, ErrorMessage = "The {0} must be between {1} and {2}.")]
    //[Required]
    public double Price { get; set; }
    //[Required]
    public int ProductionYear { get; set; }
    //[Required]
    //[StringLength(300, ErrorMessage = "Description Max Length is 300")]
    public string Description { get; set; }
    //[Required]
    public string VehicleType { get; set; }
    //[Required]
    public string Fuel { get; set; }
    //[Required]
    //[Range(0, 15000, ErrorMessage = "The {0} must be between {1} and {2}.")]
    public int CubicCapacity { get; set; }
    //[Required]
    //[Range(0, 3000, ErrorMessage = "The {0} must be between {1} and {2}.")]
    public double Power { get; set; }
    //[Required]
    public string Color { get; set; }
    //[Range(0, 9999999, ErrorMessage = "The {0} must be between {1} and {2}.")]
    public int Mileage { get; set; }
    //[Required]
    public string Transmission { get; set; }
    //[Required]
    public IEnumerable<IFormFile>? Files { get; set; }
    public Guid CarManufacturerId { get; set; }

}
