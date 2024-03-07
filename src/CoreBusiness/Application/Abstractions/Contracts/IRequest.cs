namespace Application.Abstractions.Contracts;

public interface IRequest
{
    DateTimeOffset Timestamp { get; }
}