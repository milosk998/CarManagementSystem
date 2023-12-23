namespace MKCarSales.Models.DTOs
{
    public class UploadCarImageDto
    {
        public Guid CarId { get; set; }
        public IFormFile Image { get; set; }
    }
}
