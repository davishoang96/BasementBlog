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
        if (result == null || result.IsDelete.HasValue && result.IsDelete.Value == true)
        {
            return NotFound(new { Message = "Post not found or has been deleted." });
        }

        return Ok(result);
    }

    //[Authorize(Roles = "Admin")]
    //[HttpDelete("DeletePost/{postId}")]
    //[SwaggerOperation(OperationId = "DeletePost")]
    //public async Task<bool> DeletePost(string postId)
    //{
    //    return await postRepository.DeletePost(postId);
    //}

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    [SwaggerOperation(OperationId = "SoftDeletePost")]
    public async Task<bool> SoftDeletePost(string id)
    {
        return await postRepository.SoftDeletePost(id);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("WipeAllSoftDeletedPost")]
    [SwaggerOperation(OperationId = "WipeAllSoftDeletedPost")]
    public async Task<int> WipeAllSoftDeletedPost()
    {
        return await postRepository.WipeAllSoftDeletedPost();
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("restorePost")]
    [SwaggerOperation(OperationId = "RestoreDeletedPost")]
    public async Task<bool> RestoreDeletedPost([FromBody] string id)
    {
        return await postRepository.RestoreDeletedPost(id);
    }

    [Authorize]
    [HttpPost("savePost")]
    [Produces("text/plain")]
    [SwaggerOperation(OperationId = "savePost")]
    public async Task<string> SavePost([FromBody] PostDTO postDto)
    {
        var result = await postRepository.SaveOrUpdatePost(postDto);
        return result;
    }
}
