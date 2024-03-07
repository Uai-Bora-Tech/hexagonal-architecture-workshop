using Domain.Abstractions.Aggregates;
using Domain.Entities;
using Domain.ValueObjects;
using System.Text.Json.Serialization;

namespace Domain.Aggregates;

public class Shelf : AggregateRoot
{
    private readonly List<ShelfItem> _shelfItems = new();

    public bool IsNewRegister { get; private set; } = false;

    [JsonIgnore]
    public Type EntityModified { get; private set; } = typeof(Shelf);

    public Guid Id { get; private set; }

    public bool IsActive { get; private set; }

    public bool IsDeleted { get; private set; }

    public string Title { get; private set; }

    public string Description { get; private set; }

    public Location Location { get; private set; }

    public IReadOnlyCollection<ShelfItem> Items
        => _shelfItems;

    public void CreateShelf(Guid id, string title, string description, Location location)
    {
        Id = id;
        Title = title;
        Description = description;
        Location = location;

        IsActive = true;
        IsNewRegister = true;
    }

    public void DeleteShelf()
        => IsDeleted = true;

    public void ActiveShelf()
        => IsActive = true;

    public void DeactiveShelf()
        => IsActive = false;

    public void AddShelfItem(Book book, decimal price, int quantity)
    {
        _shelfItems.Add(new(false, book, price, quantity, this));
        IsNewRegister = true;
        EntityModified = typeof(ShelfItem);
    }

    public void IncreaseShelfItem(Guid shelfItemId, int quantity)
    {
        _shelfItems.Single(item => item.Id.Equals(shelfItemId)).Increase(quantity);
        EntityModified = typeof(ShelfItem);
    }

    public void ActiveShelfItem(Guid shelfItemId)
    {
        _shelfItems.Single(item => item.Id.Equals(shelfItemId)).Activate();
        EntityModified = typeof(ShelfItem);
    }

    public void DeactiveShelfItem(Guid shelfItemId)
    {
        _shelfItems.Single(item => item.Id.Equals(shelfItemId)).Deactivate();
        EntityModified = typeof(ShelfItem);
    }

    public void ChangeShelfLocation(Location location)
        => Location = location;

    public void UpdateShelfTitle(string title)
        => Title = title;

    public void UpdateShelfDescription(string description)
        => Description = description;
}