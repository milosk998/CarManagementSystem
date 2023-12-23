namespace MKCarSales.Models;

public class CarManufacturer
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public virtual ICollection<Car> Cars { get; set; } = new List<Car>();
}
