using AutoFixture;
using BasementBlog.DTO;
using Microsoft.AspNetCore.Mvc;

namespace DevBlog.Controllers;

[ApiController]
[Route("[controller]")]
public class GetPostController : ControllerBase
{
    [HttpGet]
    public IEnumerable<PostDTO> Get()
    {
        var fixture = new Fixture();
        return fixture.Build<PostDTO>().CreateMany(50);
    }
}
