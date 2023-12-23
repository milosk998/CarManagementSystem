using MKCarSales.Models;

namespace MKCarSales.Interfaces;

public interface IMediaRepository
{
    Task<Image> UploadCarImageFileAsync(IFormFile fileToUpload);
    Task<Image> PostCarImageAsync(Image image);
    Task<Image> GetImageByIdAsync(Guid id);
    Task DeleteImageAsync(Image image);
}
