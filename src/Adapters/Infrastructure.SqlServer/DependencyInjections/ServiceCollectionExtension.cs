using Application.Ports.Repositories;
using Infrastructure.SqlServer.Databases;
using Infrastructure.SqlServer.Databases.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.SqlServer.DependencyInjections;

public static class ServiceCollectionExtension
{
    public static IServiceCollection ConfigureLibraryDbContext(this IServiceCollection services, IConfiguration configuration)
            => services.AddDbContextPool<DbContext, LibraryContext>((provider, builder) =>
            {
                builder
                    .EnableDetailedErrors()
                    .EnableSensitiveDataLogging()
                    .UseSqlServer(
                        connectionString: configuration.GetConnectionString("LibraryDb"),
                        sqlServerOptionsAction: options
                            => options.MigrationsAssembly(typeof(LibraryContext).Assembly.GetName().Name));
            });

    public static IServiceCollection ConfigureLibraryRepositories(this IServiceCollection services)
        => services.AddScoped<ILibraryRepository, LibraryRepository>();
}
