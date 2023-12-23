namespace MKCarSales.Models;

public class Car
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public int ProductionYear { get; set; }
    public string VehicleType { get; set; }
    public string Fuel { get; set; }
    public int CubicCapacity { get; set; }
    public double Power { get; set; }
    public string Color { get; set; }
    public int Mileage { get; set; }
    public string Transmission { get; set; }
    public string Description { get; set; }
    public virtual IEnumerable<Image> Images { get; set; } = new List<Image>();
    public Guid CarManufacturerId { get; set; }
    public virtual CarManufacturer CarManufacturer { get; set; } = null!;
}
