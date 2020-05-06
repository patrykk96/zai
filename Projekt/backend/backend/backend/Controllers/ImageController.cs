using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    public class ImageController : Controller
    {
        private readonly IImageService _imageService;

        public ImageController(IImageService imageService)
        {
            _imageService = imageService;
        }

        [HttpGet("api/image/{filename}")]
        public async Task<IActionResult> GetImage(string filename)
        {
            if (filename == null || filename.Length == 0)
            {
                return BadRequest();
            }

            var image = await _imageService.GetImage(filename);

            return File(image, "image/jpeg");
        }
    }
}