using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;
using asp_entity.Database;
using asp_entity.Models;
using asp_entity.Services;

namespace asp_entity.Tests;

public class ExampleQueryServiceTests
{
    [Fact]
    public async Task GetExamplesAsync_reuses_first_level_cache_within_service_scope()
    {
        var databaseName = Guid.NewGuid().ToString();
        var options = CreateOptions(databaseName);
        using var database = new ApplicationDatabaseContext(options);
        database.Example.Add(new Example { Id = 1, Text = "first" });
        await database.SaveChangesAsync();
        using var cache = new MemoryCache(new MemoryCacheOptions());
        var service = new ExampleQueryService(database, cache, NullLogger<ExampleQueryService>.Instance);

        var firstResult = await service.GetExamplesAsync();
        database.Example.RemoveRange(database.Example);
        await database.SaveChangesAsync();

        var secondResult = await service.GetExamplesAsync();

        Assert.Single(firstResult);
        Assert.Single(secondResult);
        Assert.Equal(firstResult[0].Id, secondResult[0].Id);
    }

    [Fact]
    public async Task GetExamplesAsync_reuses_second_level_cache_across_service_scopes()
    {
        var databaseName = Guid.NewGuid().ToString();
        var options = CreateOptions(databaseName);
        using var cache = new MemoryCache(new MemoryCacheOptions());

        using (var firstDatabase = new ApplicationDatabaseContext(options))
        {
            firstDatabase.Example.Add(new Example { Id = 1, Text = "cached" });
            await firstDatabase.SaveChangesAsync();
            var firstService = new ExampleQueryService(firstDatabase, cache, NullLogger<ExampleQueryService>.Instance);
            await firstService.GetExamplesAsync();
        }

        using (var secondDatabase = new ApplicationDatabaseContext(options))
        {
            secondDatabase.Example.RemoveRange(secondDatabase.Example);
            await secondDatabase.SaveChangesAsync();
            var secondService = new ExampleQueryService(secondDatabase, cache, NullLogger<ExampleQueryService>.Instance);

            var result = await secondService.GetExamplesAsync();

            Assert.Single(result);
            Assert.Equal("cached", result[0].Text);
        }
    }

    private static DbContextOptions<ApplicationDatabaseContext> CreateOptions(string databaseName) =>
        new DbContextOptionsBuilder<ApplicationDatabaseContext>()
            .UseInMemoryDatabase(databaseName)
            .Options;
}