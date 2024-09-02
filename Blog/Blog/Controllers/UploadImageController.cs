using Blog.DTO;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Blog.Controllers;

[Route("[controller]")]
public class UploadImageController : BaseAuthorizedController
{
    public UploadImageController() { }

    [HttpPost(nameof(UploadImage))]
    [Produces("text/plain")]
    [SwaggerOperation(OperationId = nameof(UploadImage))]
    public async Task<string> UploadImage([FromBody] ImageDTO imageDTO)
    {
        return imageDTO.Name;
    }
}
