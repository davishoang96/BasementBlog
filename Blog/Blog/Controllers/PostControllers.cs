﻿using AutoFixture;
using Blog.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers;

[Route("[controller]")]
[ApiController]
public class PostController : ControllerBase
{
    [HttpGet]
    [Route("GetPosts")]
    public ActionResult<IEnumerable<PostDTO>> GetPosts()
    {
        var fixture = new Fixture();
        var post = fixture.Build<PostDTO>().CreateMany(50);
        return Ok(post);
    }
}
