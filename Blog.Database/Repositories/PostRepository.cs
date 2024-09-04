using Blog.Database.Interfaces;
using Blog.Database.Models;
using Blog.DTO;
using Blog.Services.Interfaces;
using Blog.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Blog.Database.Repositories;

public class PostRepository : IPostRepository
{
    private readonly DatabaseContext db;
    private ISqidService sqidService;

    public PostRepository(DatabaseContext databaseContext, ISqidService sqidService)
    {
        this.sqidService = sqidService;
        db = databaseContext;
    }

    /// <summary>
    /// Hard delete a post by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<bool> DeletePost(string id)
    {
        var postId = sqidService.DecryptId(id);
        var model = db.Posts.SingleOrDefault(s => s.Id == postId);
        if (model is not null)
        {
            var result = db.Remove(model);
            await db.SaveChangesAsync();
            return true;
        }

        return false;
    }

    /// <summary>
    /// Mark posts as delete by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<bool> SoftDeletePost(string id)
    {
        var postId = sqidService.DecryptId(id);
        var model = db.Posts.SingleOrDefault(s => s.Id == postId);
        if (model is not null)
        {
            model.IsDeleted = true;
            db.Update(model);
            await db.SaveChangesAsync();
            return true;
        }

        return false;
    }

    /// <summary>
    /// Restore soft deleted post by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<bool> RestoreDeletedPost(string id)
    {
        var postId = sqidService.DecryptId(id);
        var model = db.Posts.SingleOrDefault(s => s.Id == postId);
        if (model is not null)
        {
            model.IsDeleted = false;
            db.Update(model);
            await db.SaveChangesAsync();
            return true;
        }

        return false;
    }

    /// <summary>
    /// Get all posts without body (lightweight posts)
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<PostDTO>> GetAllPosts()
    {
        return await db.Posts.Include(p => p.Category).Select(model => new PostDTO
        {
            Id = sqidService.EncryptId(model.Id),
            Title = model.Title,
            Body = null,
            CategoryName = model.Category != null ? model.Category.Name : null,
            PublishDate = model.PublishDate,
            Description = model.Description,
            ModifiedDate = model.ModifiedDate,
            IsDelete = model.IsDeleted,
        }).ToListAsync();
    }

    /// <summary>
    /// Get all posts without body and categories (lightweight posts)
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<PostDTO>> GetUnclassifiedPosts()
    {
        var result = db.Posts.Where(s => s.CategoryId == null && (s.IsDeleted == false || s.IsDeleted == null));

        if (result == null || result.IsNullOrEmpty())
        {
            return Enumerable.Empty<PostDTO>();
        }

        var r = new List<PostDTO>();

        foreach (var model in result)
        {
            r.Add(new PostDTO
            {
                Id = sqidService.EncryptId(model.Id),
                Title = model.Title,
                Body = null,
                PublishDate = model.PublishDate,
                Description = model.Description,
                ModifiedDate = model.ModifiedDate,
            });
        }

        return r;
    }

    public async Task<PostDTO> GetPostById(string id)
    {
        var postId = sqidService.DecryptId(id);
        var model = db.Posts.Include(s => s.Category).SingleOrDefault(s => s.Id == postId);
        if (model is null)
        {
            return default!;
        }

        return new PostDTO
        {
            Id = sqidService.EncryptId(model.Id),
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

    public async Task<string> SaveOrUpdatePost(PostDTO post)
    {
        if (post is null)
        {
            return string.Empty;
        }

        Post? postModel = null;
        Category? categoryModel = null;

        if (post.Id is not null)
        {
            var postId = sqidService.DecryptId(post.Id);
            postModel = db.Posts.Single(s => s.Id == postId);
        }

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
        return sqidService.EncryptId(postModel.Id);
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

        return Enumerable.Empty<CategoryDTO>();
    }

    public async Task<IEnumerable<CategoryDTO>> GetCategoriesWithLightPostDTO()
    {
        return await db.Categories.Include(c => c.Posts).Where(s => s.Name != PredefineCategoryNameExt.ToString(PredefineCategoryNames.WorkLogs))
        .Select(c => new CategoryDTO
        {
            CategoryId = c.CategoryId,
            Name = c.Name,
            PostDTOs = c.Posts.Where(p => p.CategoryId != null && (p.IsDeleted == null || p.IsDeleted == false))
            .Select(p => new PostDTO
            {
                Id = sqidService.EncryptId(p.Id),
                Title = p.Title,
                Body = null,
                Description = null,
                PublishDate = p.PublishDate
            })
            .ToList()
        })
        .Where(c => c.PostDTOs.Any())
        .ToListAsync();
    }

    /// <summary>
    /// Permanent delete all posts from db (Use with caution)
    /// </summary>
    /// <returns>Number of deleted post</returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<int> WipeAllSoftDeletedPost()
    {
        var posts = await db.Posts.Where(s => s.IsDeleted != null && s.IsDeleted == true).ToListAsync();
        foreach (var p in posts)
        {
            db.Remove(p);
        }

        await db.SaveChangesAsync();

        return posts.Count;
    }
}
