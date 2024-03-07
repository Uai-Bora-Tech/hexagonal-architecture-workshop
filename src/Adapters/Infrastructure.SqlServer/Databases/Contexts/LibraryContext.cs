using Domain.Aggregates;
using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.SqlServer.Databases.Contexts;

public class LibraryContext : DbContext
{
    public DbSet<Shelf>? Shelfs { get; set; }
    public DbSet<ShelfItem>? ShelfItems { get; set; }
    public DbSet<Book>? Books { get; set; }
    public DbSet<Location>? Locations { get; set; }

    public LibraryContext(DbContextOptions options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        => modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        => configurationBuilder
            .Properties<string>()
            .AreUnicode(false)
            .HaveMaxLength(1024);
}
