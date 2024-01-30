using BasementBlog.Database;
using BasementBlog.Database.Models;
using BasementBlog.DTO;
using BasementBlog.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace BasementBlog.Services;

public class PostService : IPostService
{
    private readonly DatabaseContext databaseContext;
    public PostService(DatabaseContext databaseContext)
    {
        this.databaseContext = databaseContext;
    }

    public async Task<bool> DeletePost(int id)
    {
        var model = databaseContext.Post.SingleOrDefault(s => s.Id == id);
        if (model is not null)
        {
            var result = databaseContext.Remove(model);
            await databaseContext.SaveChangesAsync();
            return true;
        }

        return false;
    }

    public async Task<IEnumerable<PostDTO>> GetAllPosts()
    {
        var result = databaseContext.Post;

        if (result == null || result.IsNullOrEmpty())
        {
            return Enumerable.Empty<PostDTO>();
        }

        var r = new List<PostDTO>();

        foreach (var model in result)
        {
            r.Add(new PostDTO
            {
                Id = model.Id,
                Title = model.Title,
                Body = model.Body,
                PublishDate = model.PublishDate,
                Description = model.Description,
                ModifiedDate = model.ModifiedDate,
            });
        }

        return r;
    }

    public async Task<PostDTO> GetPostById(int id)
    {
        var model = databaseContext.Post.SingleOrDefault(s => s.Id == id);
        if (model is null)
        {
            return default!;
        }

        return new PostDTO
        {
            Id = id,
            Title = model.Title,
            Body = model.Body,
            Description = model.Description,
            ModifiedDate = model.ModifiedDate,
            PublishDate = model.PublishDate,
        };
    }

    public async Task<bool> SaveOrUpdatePost(PostDTO post)
    {
        if (post is null)
        {
            return false;
        }

        var modelPost = databaseContext.Post.SingleOrDefault(s => s.Id == post.Id);
        if (modelPost is null)
        {
            databaseContext.Add(new Post
            {
                Title = post.Title,
                ModifiedDate = DateTime.Now,
                PublishDate = DateTime.Now,
                Description = post.Description,
                Body = post.Body,
            });
        }
        else
        {
            modelPost.Title = post.Title;
            modelPost.ModifiedDate = DateTime.Now;
            modelPost.Body = post.Body;
            modelPost.Description = post.Description;
            databaseContext.Update(modelPost);
        }

        await databaseContext.SaveChangesAsync();
        return true;
    }
}
