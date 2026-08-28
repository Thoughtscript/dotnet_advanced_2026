using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using asp_entity.Database;
using asp_entity.Models;

namespace asp_entity.Services;

/// <summary>
/// Reads <see cref="Example"/> records through a scoped first-level cache and a shared memory second-level cache.
/// </summary>
public sealed class ExampleQueryService(
    ApplicationDatabaseContext database,
    IMemoryCache secondLevelCache,
    ILogger<ExampleQueryService> logger)
{
    private const string CacheKey = "examples:all";
    private IReadOnlyList<Example>? _firstLevelCache;

    /// <summary>
    /// Returns all examples, checking the current service scope before the shared cache and database.
    /// </summary>
    /// <remarks>
    /// The first-level cache belongs to this scoped service instance. The second-level cache is supplied by
    /// <see cref="IMemoryCache"/> and can be reused by later scopes in the same application process.
    /// Cached entities are not tracked by EF Core so they can safely outlive the context that loaded them.
    /// </remarks>
    public async Task<IReadOnlyList<Example>> GetExamplesAsync(CancellationToken cancellationToken = default)
    {
        // First-level cache: avoid repeating the query during this scope.
        if (_firstLevelCache is not null)
        {
            logger.LogInformation("First-level cache hit for {CacheKey}.", CacheKey);
            return _firstLevelCache;
        }

        // Second-level cache: reuse the result loaded by an earlier scope.
        if (secondLevelCache.TryGetValue(CacheKey, out IReadOnlyList<Example>? cachedExamples)
            && cachedExamples is not null)
        {
            logger.LogInformation("Second-level cache hit for {CacheKey}; copied into the first-level cache.", CacheKey);
            _firstLevelCache = cachedExamples;
            return cachedExamples;
        }

        logger.LogInformation("Cache miss for {CacheKey}; querying the database.", CacheKey);

        // Cache detached entities because the DbContext is scoped separately from the shared cache.
        var examples = await database.Example
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        _firstLevelCache = examples;
        secondLevelCache.Set(CacheKey, examples, new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5),
            SlidingExpiration = TimeSpan.FromMinutes(1)
        });

        logger.LogInformation("Cached {Count} examples in the first- and second-level caches for {CacheKey}.", examples.Count, CacheKey);

        return examples;
    }
}