using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MKCarSales.Models;
namespace MKCarSales;

public class CarSalesDbContext : IdentityDbContext<ApplicationUser>
{
    public CarSalesDbContext(DbContextOptions<CarSalesDbContext> options) : base(options) { }
    public virtual DbSet<Car> Cars { get; set; } = null!;
    public virtual DbSet<CarManufacturer> CarManufacturers { get; set; } = null!;
    public virtual DbSet<Image> Image { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

    }
}
