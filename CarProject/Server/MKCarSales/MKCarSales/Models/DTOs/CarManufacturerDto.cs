namespace MKCarSales.Models.DTOs;

public class CarManufacturerDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public ICollection<Car> Cars { get; set; } = new List<Car>();
}
