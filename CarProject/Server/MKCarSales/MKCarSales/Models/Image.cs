namespace MKCarSales.Models;

public class Image
{
    public Guid Id { get; set; }
    public Guid carId { get; set; }
    public string FileName { get; set; }

    public string ContentType { get; set; }

    public string FilePath { get; set; }

    public string RelativePath { get; set; }
}
