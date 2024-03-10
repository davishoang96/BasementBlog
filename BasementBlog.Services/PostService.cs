using BasementBlog.Database;
using BasementBlog.Database.Models;
using BasementBlog.DTO;
using BasementBlog.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Post = BasementBlog.Database.Models.Post;

namespace BasementBlog.Services;

public class PostService : IPostService
{
    private readonly DatabaseContext db;
    public PostService(DatabaseContext databaseContext)
    {
        this.db = databaseContext;
    }

    public async Task<bool> DeletePost(int id)
    {
        var model = db.Posts.SingleOrDefault(s => s.Id == id);
        if (model is not null)
        {
            var result = db.Remove(model);
            await db.SaveChangesAsync();
            return true;
        }

        return false;
    }

    public async Task<IEnumerable<PostDTO>> GetAllPosts()
    {
        var result = db.Posts;

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
        var model = db.Posts.Include(s => s.Category).SingleOrDefault(s => s.Id == id);
        if (model is null)
        {
            return default!;
        }

        return new PostDTO
        {
            Id = id,
            Title = model.Title,
            Body = model.Body,
            CategoryId = model.CategoryId,
            CategoryName = model.Category?.Name,
            Description = model.Description,
            ModifiedDate = model.ModifiedDate,
            PublishDate = model.PublishDate,
        };
    }

    public async Task<Category> SaveOrUpdateCategory(CategoryDTO categoryDTO)
    {
        var model = categoryDTO.CategoryId == 0 ? new Category() : db.Categories.Single(s => s.CategoryId == categoryDTO.CategoryId);
        model.Name = categoryDTO.Name;

        if (model.CategoryId == 0)
        {
            db.Add(model);
        }
        else
        {
            db.Update(model);
        }

        await db.SaveChangesAsync();
        return model;
    }

    public async Task<int> SaveOrUpdatePost(PostDTO post)
    {
        if (post is null)
        {
            return -1;
        }

        Post postModel;
        Category? categoryModel = null;

        // TODO: fix this
        postModel = db.Posts.SingleOrDefault(s => s.Id == post.Id);

        if (!string.IsNullOrEmpty(post.CategoryName))
        {
            categoryModel = await SaveOrUpdateCategory(new CategoryDTO
            {
                CategoryId = post.CategoryId.HasValue ? post.CategoryId.Value : 0,
                Name = post.CategoryName
            });
        }

        if (postModel is null)
        {
            postModel = new Post
            {
                Title = post.Title,
                ModifiedDate = DateTime.Now,
                PublishDate = DateTime.Now,
                Description = post.Description,
                Category = categoryModel,
                Body = post.Body,
            };

            db.Add(postModel);
        }
        else
        {
            postModel.Title = post.Title;
            postModel.ModifiedDate = DateTime.Now;
            postModel.Body = post.Body;
            postModel.Category = categoryModel;
            postModel.Description = post.Description;
            db.Update(postModel);
        }

        await db.SaveChangesAsync();
        return postModel.Id;
    }

    public async Task<IEnumerable<CategoryDTO>> GetCategoryDTOs()
    {
        var categoryModels = await db.Categories.ToListAsync();
        if (categoryModels.Any())
        {
            var categoryDTOs = new List<CategoryDTO>();
            foreach (var category in categoryModels)
            {
                categoryDTOs.Add(new CategoryDTO
                {
                    CategoryId = category.CategoryId,
                    Name = category.Name
                });
            }

            return categoryDTOs;
        }

        return null;
    }
}
