using Application.Abstractions.Handles;
using Application.Contracts.ShelfService;
using Application.Ports.Repositories;
using Domain.Aggregates;

namespace Application.Handles.Queries;

public class GetShelfHandler : QueryHandler<Request.GetShelf, Shelf>
{
    public GetShelfHandler(ILibraryRepository repository)
        : base(repository) { }

    public override async Task<Shelf> Handle(Request.GetShelf request, CancellationToken cancellationToken)
    {
        return await _repository.GetAsync<Shelf>(request.Id, cancellationToken, shelf => shelf.Location, shelf => shelf.Items);
    }
}
