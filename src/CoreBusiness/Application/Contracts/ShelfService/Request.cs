using Application.Abstractions.Contracts;
using Application.Contracts.DataTransferObjects.ShelfDto;

namespace Application.Contracts.ShelfService;

public static class Request
{
    public record CreateShelf(Guid Id, string Title, string Description, Dto.Location Location) : Message, IRequest;

    public record GetShelf(Guid Id) : IQuery;
}
