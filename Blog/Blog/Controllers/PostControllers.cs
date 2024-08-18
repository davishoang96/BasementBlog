using AutoFixture;
using Blog.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers;

[ApiController]
[Route("[controller]")]
public class PostController : ControllerBase
{
    [Authorize]
    [HttpGet("GetPosts")]
    public ActionResult<IEnumerable<PostDTO>> GetPosts()
    {
        var fixture = new Fixture();
        var post = fixture.Build<PostDTO>().CreateMany(50);
        return Ok(post);
    }
}
