using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class ConverterController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Convert([FromForm] IFormFile image)
    {
        Console.WriteLine(image.FileName);

        string filename = image.FileName;

        using (var memoryStream = new MemoryStream())
        {
            await image.CopyToAsync(memoryStream);

            // Upload the file if less than 2 MB
            if (memoryStream.Length < 2097152)
            {
            
                byte[] filedata = memoryStream.ToArray();
                var cd = new System.Net.Mime.ContentDisposition
                {
                    FileName = filename,
                    Inline = true,
                };
                Response.Headers.Add("Content-Disposition", cd.ToString());
                return Ok(File(filedata, image.ContentType));
            }
            else
            {
                return  BadRequest("The file is too large.");
            }
        }
    }

}