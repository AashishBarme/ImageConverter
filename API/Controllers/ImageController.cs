using System;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class ImageController : ControllerBase
{
    private readonly ILogger<ImageController> _logger;

    public ImageController(ILogger<ImageController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> Convert([FromForm] IFormFile image, string format)
    {
        var fileName = image.FileName.Split('.')[0];
        using (var memoryStream = new MemoryStream())
        {
            await image.CopyToAsync(memoryStream);
                var x = new Converter.ImageConverter();
                memoryStream.Position = 0;
                byte[] filedata = x.Convert(memoryStream, format);   
                var cd = new System.Net.Mime.ContentDisposition
                {
                    FileName = $"{fileName}.{format}",
                    Inline = true,
                };
                Response.Headers.Add("Content-Disposition", cd.ToString());
                _logger.LogInformation(image.FileName);
                return File(filedata, $"image/{format}");
        }

    }

}