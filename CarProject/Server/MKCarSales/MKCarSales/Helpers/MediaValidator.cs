using System.ComponentModel.DataAnnotations;

namespace MKCarSales.Helpers;

public static class MediaValidator
{
    private static readonly string[] _allowedImageExtensions = { ".jpg", ".jpeg", ".png", ".webp" };

    private static readonly string[] _allowedVideoExtensions = { ".webm", ".mp4", ".mov", ".m4v" };

    // 10 mb max size
    private static long _maxFileSize = 10 * 1024 * 1024;

    public static void ValidateMedia(IFormFile file)
    {
        var typeOfFile = file.ContentType.Substring(0, 5);
        var extension = Path.GetExtension(file.FileName);

        if (typeOfFile == "image")
        {
            ValidateImageExtension(extension);
        }

        if (typeOfFile == "video")
        {
            ValidateVideoExtension(extension);
        }

        ValidateFileSize(file.Length);
    }

    public static void ValidateImage(IFormFile file)
    {
        ValidateImageExtension(Path.GetExtension(file.FileName));
        ValidateFileSize(file.Length);
    }

    private static void ValidateFileSize(long fileSize)
    {
        if (fileSize > _maxFileSize)
        {
            throw new ValidationException($"Maximum allowed file size is {_maxFileSize} bytes.");
        }
    }

    private static void ValidateImageExtension(string extension)
    {
        if (!_allowedImageExtensions.Contains(extension.ToLower()))
        {
            throw new ValidationException("This image extension is not allowed!");
        }
    }

    private static void ValidateVideoExtension(string extension)
    {
        if (!_allowedVideoExtensions.Contains(extension.ToLower()))
        {
            throw new ValidationException("This video extension is not allowed!");
        }
    }
}