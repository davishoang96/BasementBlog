using AutoFixture;
using Blog.Database.Interfaces;
using Blog.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Blog.Controllers;

[ApiController]
[Route("[controller]")]
public class PostController : ControllerBase
{
    private IPostRepository postRepository;
    public PostController(IPostRepository postRepository)
    {
        this.postRepository = postRepository;
    }

    [HttpGet("GetPosts")]
    [SwaggerOperation(OperationId = "GetPosts")]
    public async Task<ActionResult<IEnumerable<PostDTO>>> GetPosts()
    {
        var posts = await postRepository.GetAllPosts();
        return Ok(posts);
    }

    [HttpGet("GetCategoriesWithLightPost")]
    [SwaggerOperation(OperationId = "GetCategoriesWithLightPost")]
    public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategoriesWithLightPost()
    {
        var result = await postRepository.GetCategoriesWithLightPostDTO();
        return Ok(result);
    }

    [HttpGet("GetCategory")]
    [SwaggerOperation(OperationId = "GetCategory")]
    public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategoryDTOs()
    {
        var result = await postRepository.GetCategoryDTOs();
        return Ok(result);
    }

    [HttpGet("GetUnclassifiedPosts")]
    [SwaggerOperation(OperationId = "GetUnclassifiedPosts")]
    public async Task<ActionResult<IEnumerable<PostDTO>>> GetUnclassifiedPosts()
    {
        var result = await postRepository.GetUnclassifiedPosts();
        return Ok(result);
    }

    [HttpGet("{id}")]
    [SwaggerOperation(OperationId = "GetPostById")]
    public async Task<ActionResult<PostDTO>> GetPostById(string id)
    {
        var result = await postRepository.GetPostById(id);
        return Ok(result);
    }

    [Authorize]
    [HttpPost("savePost")]
    [SwaggerOperation(OperationId = "savePost")]
    public async Task<ActionResult<string>> SavePost([FromBody] PostDTO postDto)
    {
        var result = await postRepository.SaveOrUpdatePost(postDto);
        return Ok(result);
    }
}
