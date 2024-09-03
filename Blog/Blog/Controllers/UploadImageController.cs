using Blog.DTO;
using Microsoft.AspNetCore.Mvc;
using SkiaSharp;
using Swashbuckle.AspNetCore.Annotations;
using System.Drawing;

namespace Blog.Controllers;

[Route("[controller]")]
public class UploadImageController : BaseAuthorizedController
{
    private readonly IConfiguration configuration;
    public UploadImageController(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    [HttpPost(nameof(UploadImage))]
    [Produces("text/plain")]
    [SwaggerOperation(OperationId = nameof(UploadImage))]
    public async Task<string> UploadImage([FromBody] ImageDTO imageDTO)
    {
        var dict = configuration["ImageDirectoryPath"];
        if (!Path.Exists(dict))
        {
            Directory.CreateDirectory(dict);
        }

        Guid guid = Guid.NewGuid();
        imageDTO.Name = guid.ToString();
        var filePath = Path.Combine(dict, $"{imageDTO.Name}.jpg");
        byte[] imageBytes = Convert.FromBase64String(imageDTO.Data);

        // Use skiasharp to write imageBytes to disk
        using (var stream = new SKMemoryStream(imageBytes))
        using (var bitmap = SKBitmap.Decode(stream))
        using (var image = SKImage.FromBitmap(bitmap))
        using (var data = image.Encode(SKEncodedImageFormat.Jpeg, 100)) // 100 is the quality
        using (var fileStream = System.IO.File.OpenWrite(filePath))
        {
            data.SaveTo(fileStream);
        }

        return $"![Image-{DateTime.Now.ToShortDateString()}](/Images/{imageDTO.Name}.jpg){{ width={100}% }}"; ;
    }
}
