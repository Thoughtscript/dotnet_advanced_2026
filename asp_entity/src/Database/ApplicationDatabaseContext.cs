using Microsoft.EntityFrameworkCore;
using asp_entity.Models;

namespace asp_entity.Database;

// Helpful minimal example: https://github.com/grpm98/pisofinderapi/blob/f114d5860c60267a05ccfd8ed41c162ce8e51224/Data/PisoFinderContext.cs
public class ApplicationDatabaseContext : DbContext
{
    public DbSet<Example> Example { get; set; }

    public ApplicationDatabaseContext(DbContextOptions<ApplicationDatabaseContext> options)
    : base(options)
    {
    }

    public ApplicationDatabaseContext()
    {
    }
}