using Application.Abstractions.Handles;
using Application.Contracts.ShelfService;
using Application.Handles.Commands;
using Application.Handles.Queries;
using Application.Ports.Handles;
using Domain.Aggregates;
using Microsoft.Extensions.DependencyInjection;

namespace Application.DependencyInjections.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddConfigureHandlers(this IServiceCollection services)
    {
        services.AddTransient<ICommandHandler<Request.CreateShelf>, CreateShelfHandler>();
        services.AddTransient<IQueryHandler<Request.GetShelf, Shelf>, GetShelfHandler>();
    }
}
