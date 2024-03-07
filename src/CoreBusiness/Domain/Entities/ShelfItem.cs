using Domain.Abstractions.Entities;
using Domain.Aggregates;
using Domain.ValueObjects;
using System;

namespace Domain.Entities;

public class ShelfItem : Entity
{
    protected ShelfItem() { }

    public ShelfItem(bool isActive, Book book, decimal price, int quantity, Shelf shelf)
    {
        Id = Guid.NewGuid();
        IsActive = isActive;
        Book = book;
        Price = price;
        Quantity = quantity;
        Shelf = shelf;
        ShelfId = shelf.Id;
        IsModified = true;
    }

    public bool IsModified { get; private set; } = false;

    public Guid Id { get; }

    public bool IsActive { get; private set; } = true;

    public Book Book { get; }

    public decimal Price { get; }

    public int Quantity { get; private set; }

    public void Increase(int quantity)
    {
        Quantity += quantity;
        IsModified = true;
    }

    public void Decrease(int quantity)
    {
        Quantity -= quantity;
        IsModified = true;
    }

    public void Activate()
    {
        IsActive = true;
        IsModified = true;
    }

    public void Deactivate()
    {
        IsActive = false;
        IsModified = true;
    }

    #region Reference to navigation
    public Guid ShelfId { get; }
    public Shelf Shelf { get; } = null!;
    #endregion
}
