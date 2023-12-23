using MKCarSales.Models;
using MKCarSales.Models.DTOs;

namespace MKCarSales.Interfaces;

public interface IMediaService
{
    Task<Image> UploadCarImageAsync(IFormFile fileToUpload);
    Task<Image> PostCarImageAsync(UploadCarImageDto uploadCarImageDto);
    Task DeleteCarImageAsync(Guid id);
}
