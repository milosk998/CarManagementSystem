using System.ComponentModel.DataAnnotations;

namespace MKCarSales.Models.DTOs
{
    public class FilterCarDto
    {
        public double? MinPrice { get; set; } = 0;
        public double? MaxPrice { get; set; } = 999999;
         public int? MinProductionYear { get; set; }
         public int? MaxProductionYear { get; set; }
         public string? Fuel { get; set; }
        public int? MinCubicCapacity { get; set; } = 0;
        public int? MaxCubicCapacity { get; set; } = 15000;
        public double? MinPower { get; set; } = 0;
        public double? MaxPower { get; set; } = 3000;
         public int? MinMileage { get; set; } = 0;
         public int? MaxMileage { get; set; } = 9999999;
         public string? Transmission { get; set; }
        [Required]
        public Guid CarManufacturerId { get; set; }
    }
}
