using Microsoft.EntityFrameworkCore;
using MKCarSales.Interfaces;
using MKCarSales.Models;

namespace MKCarSales.Repositories;

public class ImageRepository : IMediaRepository
{
    private readonly IWebHostEnvironment _webHostEnvironment;
    private const string UploadsFolderName = "Uploads";
    private readonly CarSalesDbContext _context;

    public ImageRepository(IWebHostEnvironment webHostEnvironment, CarSalesDbContext context)
    {
        _webHostEnvironment = webHostEnvironment;
        _context = context;
    }
    public async Task<Image> UploadCarImageFileAsync(IFormFile fileToUpload)
    {
        var rootFilePath = _webHostEnvironment.WebRootPath;

        var fileId = Guid.NewGuid();
        var fileName = Path.GetFileName(fileToUpload.FileName);
        var extension = Path.GetExtension(fileToUpload.FileName);

        var uploadDirectoryPath = $"{rootFilePath}{Path.DirectorySeparatorChar}{UploadsFolderName}";

        if (!Directory.Exists(uploadDirectoryPath))
        {
            Directory.CreateDirectory(uploadDirectoryPath);
        }

        var relativePath = $"{UploadsFolderName}{Path.DirectorySeparatorChar}{fileId}{extension}";
        var fullPath = $"{rootFilePath}{Path.DirectorySeparatorChar}{relativePath}";

        using (var stream = File.Create(fullPath))
        {
            await fileToUpload.CopyToAsync(stream);
        }

        return new Image
        {
            Id = fileId,
            FileName = fileName,
            FilePath = fullPath,
            RelativePath = relativePath,
            ContentType = fileToUpload.ContentType,
        };
    }

    public async Task<Image> PostCarImageAsync(Image image)
    {
        var createdImage = await _context.Image.AddAsync(image);
        await _context.SaveChangesAsync();

        return createdImage.Entity;
    }


    public async Task<Image> GetImageByIdAsync(Guid id)
    {
        return await _context.Image
             .AsNoTracking()
            .FirstOrDefaultAsync(image => image.Id == id);
    }

    public async Task DeleteImageAsync(Image image)
    {
        var rootFilePath = _webHostEnvironment.WebRootPath;

        var relativePath = $"{UploadsFolderName}{Path.DirectorySeparatorChar}{image.Id}{Path.GetExtension(image.FileName)}";
        var fullPath = $"{rootFilePath}{Path.DirectorySeparatorChar}{relativePath}";

        if (File.Exists(fullPath))
        {
            File.Delete(fullPath);
        }
        else
        {
            throw new Exception("Failed to delete the image. An error occurred during the deletion process.");
        }

        _context.Image.Remove(image);
        await _context.SaveChangesAsync();
    }
}
