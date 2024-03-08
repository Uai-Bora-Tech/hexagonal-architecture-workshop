using Application.DependencyInjections.Extensions;
using Infrastructure.SqlServer.DependencyInjections;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System.Reflection;
using Infrastructure.SqlServer.Databases.Contexts;
using Microsoft.EntityFrameworkCore;
using WebApi.Transformers;

var builder = WebApplication.CreateBuilder(args);

builder.Host
    .UseDefaultServiceProvider((context, provider) =>
    {
        provider.ValidateScopes =
            provider.ValidateOnBuild =
                context.HostingEnvironment.IsDevelopment();
    })
    .ConfigureAppConfiguration((context, configurationBuilder) =>
    {
        configurationBuilder
            .AddEnvironmentVariables();
    })
    .ConfigureServices((context, services) =>
    {
        services.AddRouting(options => options.LowercaseUrls = false);

        services.AddControllers(options =>
        {
            options.Conventions.Add(new RouteTokenTransformerConvention(new SlugyParametersTransformer()));
            options.SuppressAsyncSuffixInActionNames = true;
        });

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services
            .ConfigureLibraryDbContext(context.Configuration)
            .ConfigureLibraryRepositories();

        services
            .AddConfigureHandlers();

        services.AddCors();

        services.AddCors(options =>
        {
            options.AddPolicy("AllowAnyOrigin", builder =>
            {
                builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
            });
        });
    });

var app = builder.Build();

using var scope = app.Services.CreateScope();
var db = scope.ServiceProvider.GetService<LibraryContext>();
db.Database.Migrate();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors("AllowAnyOrigin");

await app.RunAsync();