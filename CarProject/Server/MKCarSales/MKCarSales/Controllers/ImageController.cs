using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MKCarSales.Interfaces;
using MKCarSales.Models.DTOs;
using MKCarSales.Services;

namespace MKCarSales.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IMediaService _mediaService;

        public ImageController(IMediaService mediaService)
        {
            _mediaService = mediaService;
        }
        [HttpPost]
        public async Task<IActionResult> PostMedia([FromForm] UploadCarImageDto uploadCarImageDto)
        {
            var image = await _mediaService.PostCarImageAsync(uploadCarImageDto);

            return Ok(image);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedia(Guid id)
        {
            await _mediaService.DeleteCarImageAsync(id);

            return NoContent();
        }
    }
}
