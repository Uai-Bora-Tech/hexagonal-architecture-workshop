using Domain.Aggregates;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.SqlServer.Databases.Configurations;

public class ShelfConfiguration : IEntityTypeConfiguration<Shelf>
{
    public void Configure(EntityTypeBuilder<Shelf> builder)
    {
        builder.ToTable(nameof(Shelf));

        builder.HasKey(nameof(Shelf.Id));

        builder.Ignore(prop => prop.IsNewRegister);
        builder.Ignore(prop => prop.EntityModified);

        builder
            .Property(prop => prop.IsActive)
            .IsRequired();

        builder
            .Property(prop => prop.IsDeleted)
            .IsRequired();

        builder
            .Property(prop => prop.Title)
            .HasMaxLength(200)
            .IsRequired();

        builder
            .Property(prop => prop.Description)
            .HasMaxLength(500)
            .IsRequired();

        builder
            .HasMany(shelf => shelf.Items)
            .WithOne(shelfItem => shelfItem.Shelf)
            .HasForeignKey(shelfItem => shelfItem.ShelfId)
            .IsRequired();

        builder
            .OwnsOne(
                prop => prop.Location,
                locationNavigationBuilder =>
                {
                    locationNavigationBuilder.ToTable(nameof(Location));

                    locationNavigationBuilder.Property<int>("Id").IsRequired();
                    locationNavigationBuilder.HasKey("Id");

                    locationNavigationBuilder
                        .Property(nameof(Location.Session))
                        .IsRequired()
                        .HasMaxLength(100);

                    locationNavigationBuilder
                        .Property(nameof(Location.Hall))
                        .IsRequired();

                    locationNavigationBuilder
                        .Property(nameof(Location.Bookcase))
                        .IsRequired();

                    locationNavigationBuilder
                        .Property(nameof(Location.Rack))
                        .IsRequired();
                });
    }
}
