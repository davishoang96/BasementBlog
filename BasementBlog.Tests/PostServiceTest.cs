using AutoFixture;
using BasementBlog.Database;
using BasementBlog.Database.Models;
using BasementBlog.DTO;
using BasementBlog.Services;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Post = BasementBlog.Database.Models.Post;

namespace BasementBlog.Tests;

public sealed class PostServiceTest : BaseDataContextTest
{
    [Fact]
    public async Task AddPostOK()
    {
        // Arrange
        var service = new PostService(new DatabaseContext(_dbContextOptions));

        // Act
        var id = await service.SaveOrUpdatePost(new PostDTO
        {
            Body = "Hello",
            Description = "Hello",
            Title = "Hello",
            PublishDate = DateTime.Now,
            ModifiedDate = DateTime.Now,
        });

        // Assert
        id.Should().NotBe(0);
        using (var context = new DatabaseContext(_dbContextOptions))
        {
            var model = context.Posts.SingleOrDefault(s => s.Id == id);
            model.Body.Should().Be("Hello");
        }
    }

    [Fact]
    public async Task AddPostWithCategory()
    {
        // Arrange
        var service = new PostService(new DatabaseContext(_dbContextOptions));

        // Act
        var id = await service.SaveOrUpdatePost(new PostDTO
        {
            Body = "Hello",
            CategoryName = "Hello",
            Description = "Hello",
            Title = "Hello",
            PublishDate = DateTime.Now,
            ModifiedDate = DateTime.Now,
        });

        using (var context = new DatabaseContext(_dbContextOptions))
        {
            var rs = context.Posts
                .Include(s => s.Category)
                .FirstOrDefault(s => s.Id == id);

            var ct = context.Categories.Count();
        }
    }

    [Fact]
    public async Task UpdatePostOK()
    {
        // Arrange
        var post = new Post
        {
            Id = 19,
            Description = "Hello",
            Body = "Hello",
            Title = "Hello",
            PublishDate = DateTime.Now,
            ModifiedDate = DateTime.Now,
        };

        using (var context = new DatabaseContext(_dbContextOptions))
        {
            context.Posts.Add(post);
            await context.SaveChangesAsync();

            var thePost = context.Posts.SingleOrDefault(s => s.Id == 19);

            // Assume
            thePost.Description.Should().Be("Hello");
        }

        using (var context = new DatabaseContext(_dbContextOptions))
        {
            var description = post.Description = "Test";
            var body = post.Body = "I'm testing something new";
            var title = post.Title = "Davis is testing";

            // Act
            context.Update(post);
            await context.SaveChangesAsync();

            // Assert
            var thePost = context.Posts.SingleOrDefault(s => s.Id == 19);
            thePost.Title.Should().Be(title);
            thePost.Description.Should().Be(description);
            thePost.Body.Should().Be(body);
        }
    }

    [Fact]
    public async Task GetCategoryOK()
    {
        // Arrange
        var fixture = new Fixture();
        var categoryModel = fixture.Build<Category>()
            .OmitAutoProperties()
            .With(s => s.Name)
            .With(s => s.CategoryId)
            .Without(s => s.Posts)
            .CreateMany(5);

        using (var context = new DatabaseContext(_dbContextOptions))
        {
            context.AddRange(categoryModel);
            await context.SaveChangesAsync();
        }

        var service = new PostService(new DatabaseContext(_dbContextOptions));

        // Act
        var result = await service.GetCategoryDTOs();

        // Assert
        result.Should().NotBeNull();
        result.Count().Should().Be(5);
    }
}
