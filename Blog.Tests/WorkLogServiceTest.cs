using AutoFixture;
using Blog.Database;
using Blog.Database.Models;
using Blog.Database.Repositories;
using Blog.DTO;
using Blog.Services;
using Blog.Services.Interfaces;
using Blog.Utilities;
using FluentAssertions;
using Moq;
using Xunit;

namespace Blog.Tests;

public sealed class WorkLogRepositoryTest : BaseDataContextTest
{
   private Mock<ISqidService> MockSqidService;

   public WorkLogRepositoryTest()
   {
       MockSqidService = new Mock<ISqidService>();
   }

   [Fact]
   public async Task ClearAllWorkLogsOK()
   {
        // Arrange
        var fixture = new Fixture();
        var id = 100;
        var workLogs = fixture.Build<WorkLog>().With(s => s.Id, () => id++).CreateMany(10);

        using (var context = new DatabaseContext(Options))
        {
            context.WorkLogs.AddRange(workLogs);
            await context.SaveChangesAsync();
        };

        var service = new WorkLogRepository(new DatabaseContext(Options), MockSqidService.Object);

        // Act
        var result = await service.ClearAllWorkLogs();

        // Assert
        result.Should().BeTrue();
        using (var context = new DatabaseContext(Options))
        {
            context.WorkLogs.Should().BeEmpty();
        }
   }

   [Fact]
   public async Task GetAllWorkLogOK()
   {
        // Arrange
        var fixture = new Fixture();
        var id = 100;
        var workLogs = fixture.Build<WorkLog>().With(s => s.Id, () => id++).CreateMany(10);

        using (var context = new DatabaseContext(Options))
        {
            context.WorkLogs.AddRange(workLogs);
            await context.SaveChangesAsync();
        }

        var service = new WorkLogRepository(new DatabaseContext(Options), MockSqidService.Object);

        // Act
        var result = await service.GetAllWorkLogs();

        // Assert
        result.Count().Should().Be(10);
   }

   [Fact]
   public async Task GetAllWorkLogWithoutBodyOK()
   {
        // Arrange
        var fixture = new Fixture();
        var id = 100;
        var workLogs = fixture.Build<WorkLog>().With(s => s.Id, () => id++).CreateMany(10);

        using (var context = new DatabaseContext(Options))
        {
            context.WorkLogs.AddRange(workLogs);
            await context.SaveChangesAsync();
        }

        var service = new WorkLogRepository(new DatabaseContext(Options), MockSqidService.Object);

        // Act
        var result = await service.GetAllWorkLogsWithoutBody();

        // Assert
        result.Count().Should().Be(10);
        result.All(s=>s.Body == null).Should().BeTrue();
   }

   [Fact]
   public async Task GetWorkLogByIdOK()
   {
        // Arrange
        var fixture = new Fixture();
        var worklog = fixture.Build<WorkLog>().With(s => s.Id, 1).Create();

        using (var context = new DatabaseContext(Options))
        {
            context.WorkLogs.Add(worklog);
            await context.SaveChangesAsync();
        }

        var service = new WorkLogRepository(new DatabaseContext(Options), MockSqidService.Object);
        MockSqidService.Setup(s => s.DecryptId(It.IsAny<string>())).Returns(1);
        MockSqidService.Setup(s => s.EncryptId(It.IsAny<int>())).Returns("%34oijf");

        // Act
        var result = await service.GetWorkLogById("%34oijf");

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be("%34oijf");
        result.Body.Should().Be(worklog.Body);
   }

   [Fact]
   public async Task SaveWorkLogOK()
   {
        // Arrange
        var service = new WorkLogRepository(new DatabaseContext(Options), MockSqidService.Object);
        MockSqidService.Setup(s => s.DecryptId(It.IsAny<string>())).Returns(1);

        var dto = new WorkLogDTO
        {
            Body = "Hello",
            LoggedDate = DateTime.Now.ToString(Common.DefaultDateTimeFormat),
        };

        // Act
        var result = await service.SaveOrUpdateWorkLog(dto);

        // Assert
        using (var context = new DatabaseContext(Options))
        {
            context.WorkLogs.Should().HaveCount(1);
            var model = context.WorkLogs.First();
            model.Body.Should().Be(dto.Body);
            model.LoggedDate.Should().Be(dto.LoggedDate);
        }
   }

   [Fact]
   public async Task UpdateWorkLogOK()
   {
        // Arrange
        var fixture = new Fixture();
        var worklog = fixture.Build<WorkLog>()
                             .With(s => s.Body, "Homework")
                             .With(s => s.Id, 1).Create();

        using (var context = new DatabaseContext(Options))
        {
            context.WorkLogs.Add(worklog);
            await context.SaveChangesAsync();
        }

        var dto = new WorkLogDTO
        {
            Id = ";aoerijg",
            Body = "Hello",
        };

        MockSqidService.Setup(s => s.DecryptId(It.IsAny<string>())).Returns(1);
        var service = new WorkLogRepository(new DatabaseContext(Options), MockSqidService.Object);

        // Act
        var result = await service.SaveOrUpdateWorkLog(dto);

        // Assert
        using (var context = new DatabaseContext(Options))
        {
            context.WorkLogs.Should().HaveCount(1);
            var model = context.WorkLogs.First();
            model.Id.Should().Be(1);
            model.Body.Should().Be(dto.Body);
        }
   }
}
