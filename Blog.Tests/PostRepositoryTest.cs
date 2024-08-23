using AutoFixture;
using Blog.Database;
using Blog.Database.Models;
using Blog.Database.Repositories;
using Blog.DTO;
using Blog.Services.Interfaces;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace Blog.Tests;

public sealed class PostRepositoryTest : BaseDataContextTest
{
    private Mock<ISqidService> MockSqidService;

    public PostRepositoryTest()
    {
        MockSqidService = new Mock<ISqidService>();
    }

    [Fact]
    public async Task GetCategoriesWithPosts()
    {
        // Arrange
        var fixture = new Fixture();
        var postId = 100;
        var posts = fixture.Build<Post>()
            .Without(s => s.Category)
            .With(s => s.Id, () => postId++)
            .With(s => s.IsDeleted, false)
            .With(s => s.CategoryId, 25).CreateMany(17);

        using (var context = new DatabaseContext(Options))
        {
            context.Categories.Add(new Category
            {
                CategoryId = 25,
                Name = "Programming",
            });

            context.Posts.AddRange(posts);
            await context.SaveChangesAsync();
        }

        var service = new PostRepository(new DatabaseContext(Options), MockSqidService.Object);

        // Act
        var result = await service.GetCategoriesWithLightPostDTO();

        // Assert
        result.SingleOrDefault(s => s.CategoryId == 25).PostDTOs.Count().Should().Be(17);
    }

    [Fact]
    public async Task AddPostOK()
    {
        // Arrange
        var service = new PostRepository(new DatabaseContext(Options), MockSqidService.Object);
        MockSqidService.Setup(s => s.EncryptId(It.IsAny<int>())).Returns("GOAIwe");
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
        id.Should().Be("GOAIwe");
        using (var context = new DatabaseContext(Options))
        {
            var model = context.Posts.SingleOrDefault(s => s.Title == "Hello");
            model.Body.Should().Be("Hello");
        }
    }

    [Fact]
    public async Task AddPostWithCategory()
    {
        // Arrange
        var service = new PostRepository(new DatabaseContext(Options), MockSqidService.Object);
        MockSqidService.Setup(s => s.EncryptId(It.IsAny<int>())).Returns("GOAIwe");

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

        using (var context = new DatabaseContext(Options))
        {
            var rs = context.Posts
                .Include(s => s.Category)
                .FirstOrDefault(s => s.Title == "Hello");

            var ct = context.Categories.Count();
        }
    }

    // TODO: Rewrite this unit test
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

        MockSqidService.Setup(s => s.EncryptId(It.IsAny<int>())).Returns("GOAIwe");

        using (var context = new DatabaseContext(Options))
        {
            context.Posts.Add(post);
            await context.SaveChangesAsync();

            var thePost = context.Posts.SingleOrDefault(s => s.Id == 19);

            // Assume
            thePost.Description.Should().Be("Hello");
        }

        using (var context = new DatabaseContext(Options))
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

        using (var context = new DatabaseContext(Options))
        {
            context.AddRange(categoryModel);
            await context.SaveChangesAsync();
        }

        var service = new PostRepository(new DatabaseContext(Options), MockSqidService.Object);

        // Act
        var result = await service.GetCategoryDTOs();

        // Assert
        result.Should().NotBeNull();
        result.Count().Should().Be(5);
    }

    [Fact]
    public async Task GetPostWithoutBody()
    {
        // Arrange
        var fixture = new Fixture();
        var id = 1000;

        var posts = fixture.Build<Post>().With(s => s.Id, () => id++)
            .Without(s => s.Category)
            .Without(s => s.CategoryId)
            .CreateMany(5);

        var deletedPosts = fixture.Build<Post>().With(s => s.Id, () => id++)
            .Without(s => s.Category)
            .Without(s => s.CategoryId)
            .With(s => s.IsDeleted, true)
            .CreateMany(5);

        using (var context = new DatabaseContext(Options))
        {
            context.AddRange(posts);
            context.AddRange(deletedPosts);
            await context.SaveChangesAsync();
        }

        var service = new PostRepository(new DatabaseContext(Options), MockSqidService.Object);

        // Act
        var result = await service.GetAllPosts();

        // Assert
        result.All(s => s.Body == null).Should().BeTrue();
    }

    [Fact]
    public async Task SoftDeletePost()
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

        MockSqidService.Setup(s => s.DecryptId("TEST91")).Returns(19);

        using (var context = new DatabaseContext(Options))
        {
            await context.Posts.AddAsync(post);
            await context.SaveChangesAsync();
        }

        var service = new PostRepository(new DatabaseContext(Options), MockSqidService.Object);

        // Act
        var result = service.SoftDeletePost("TEST91");

        // Assert
        using (var context = new DatabaseContext(Options))
        {
            var thePost = context.Posts.SingleOrDefault(s => s.Id == 19);
            thePost.IsDeleted.Should().BeTrue();
        }
    }

    [Fact]
    public async Task DeleteAllPostOK()
    {
        // Arrange
        var fixture = new Fixture();
        var id = 1000;

        var posts = fixture.Build<Post>()
            .Without(s => s.Category)
            .Without(s => s.CategoryId)
            .With(s => s.IsDeleted, true)
            .With(s => s.Id, () => id++).CreateMany(50);

        using (var context = new DatabaseContext(Options))
        {
            await context.Posts.AddRangeAsync(posts);
            await context.SaveChangesAsync();
        }

        var service = new PostRepository(new DatabaseContext(Options), MockSqidService.Object);

        // Act
        await service.WipeAllSoftDeletedPost();

        // Assert
        using (var context = new DatabaseContext(Options))
        {
            context.Posts.Count().Should().Be(2);
        }
    }
}
