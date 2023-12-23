using Microsoft.AspNetCore.Server.IIS.Core;
using MKCarSales.Helpers;
using MKCarSales.Interfaces;
using MKCarSales.Models;
using MKCarSales.Models.DTOs;
using MKCarSales.Repositories;

namespace MKCarSales.Services;

public class ImageService : IMediaService
{
    private readonly IMediaRepository _mediaRepository;
    private readonly ICarRepository _carRepository;

    public ImageService(IMediaRepository mediaRepository, ICarRepository carRepository)
    {
        _carRepository = carRepository;
        _mediaRepository = mediaRepository;
    }
    public async Task<Image> UploadCarImageAsync(IFormFile fileToUpload)
    {
        MediaValidator.ValidateMedia(fileToUpload);

        var image = await _mediaRepository.UploadCarImageFileAsync(fileToUpload);
        

        return image;
    }
    public async Task<Image> PostCarImageAsync(UploadCarImageDto uploadCarImageDto)
    {
        MediaValidator.ValidateMedia(uploadCarImageDto.Image);

        var image = await _mediaRepository.UploadCarImageFileAsync(uploadCarImageDto.Image);

        var car = await _carRepository.GetByIdAsync(uploadCarImageDto.CarId);

        if(car == null)
        {
            throw new Exception("Bad request. Car provided does not exist");
        }

        image.carId = car.Id;
        await _mediaRepository.PostCarImageAsync(image);

        return image;
    }
    public async Task DeleteCarImageAsync(Guid id)
    {
        if (id == Guid.Empty || id == null)
        {
            throw new ArgumentNullException(nameof(id));
        }

        var image = await _mediaRepository.GetImageByIdAsync(id);

        if (image == null)
        {
            throw new Exception("Bad request. Image provided does not exist");
        }

        await _mediaRepository.DeleteImageAsync(image);
    }
}
