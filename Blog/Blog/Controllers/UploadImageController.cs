﻿using Blog.DTO;
using Microsoft.AspNetCore.Mvc;
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
        using (MemoryStream ms = new MemoryStream(imageBytes))
        {
            // Create an Image object from the MemoryStream
            Image image = Image.FromStream(ms);
            image.Save(filePath);
            return $"![Image-{DateTime.Now.ToShortDateString()}](/Images/{imageDTO.Name}.jpg){{ width={100}% }}"; ;
        }
    }
}