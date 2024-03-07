using Domain.Aggregates;
using Xunit;

namespace UnitTest;

public class ShelfTest
{
    [Fact]
    public void Create_IsDeleteFalse_Success()
    {
        var shelf = new Shelf();
        shelf.CreateShelf(Guid.NewGuid(), null, null, null);

        Assert.False(shelf.IsDeleted);
    }

    [Fact]
    public void Create_DeleteShelf_Success()
    {
        var shelf = new Shelf();
        shelf.CreateShelf(Guid.NewGuid(), null, null, null);
        shelf.DeleteShelf();

        Assert.True(shelf.IsDeleted);
    }
}
