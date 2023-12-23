using System.ComponentModel.DataAnnotations;

namespace MKCarSales.Models.DTOs;

public class UpdateCarDto
{
    public string Name { get; set; }
    [Range(0, 999999, ErrorMessage = "The {0} must be between {1} and {2}.")]
    public double Price { get; set; }
    public int ProductionYear { get; set; }
    [StringLength(300, ErrorMessage = "Description Max Length is 300")]
    public string Description { get; set; }
    public string VehicleType { get; set; }
    public string Fuel { get; set; }
    [Range(0, 15000, ErrorMessage = "The {0} must be between {1} and {2}.")]
    public int CubicCapacity { get; set; }
    [Range(0, 3000, ErrorMessage = "The {0} must be between {1} and {2}.")]
    public double Power { get; set; }
    public string Color { get; set; }
    [Range(0, 9999999, ErrorMessage = "The {0} must be between {1} and {2}.")]
    public int Mileage { get; set; }
    public string Transmission { get; set; }
    public Guid CarManufacturerId { get; set; }
}
