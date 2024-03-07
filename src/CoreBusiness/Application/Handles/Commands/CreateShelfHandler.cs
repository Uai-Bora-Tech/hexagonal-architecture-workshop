using Application.Abstractions.Handles;
using Application.Contracts.ShelfService;
using Application.Ports.Repositories;
using Domain.Aggregates;
using Domain.ValueObjects;

namespace Application.Handles.Commands;

public class CreateShelfHandler : CommandHandler<Request.CreateShelf>
{
    public CreateShelfHandler(ILibraryRepository repository)
        : base(repository) { }

    public override async Task Handle(Request.CreateShelf request, CancellationToken cancellationToken)
    {
        var shelf = new Shelf();
        Location location = new(request.Location.Session, request.Location.Hall, request.Location.Bookcase, request.Location.Rack);
        shelf.CreateShelf(request.Id, request.Title, request.Description, location);

        await _repository.InsertAsync(shelf, cancellationToken);
    }
}