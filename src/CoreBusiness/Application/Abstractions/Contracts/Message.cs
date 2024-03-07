namespace Application.Abstractions.Contracts;

public record Message : IRequest
{
    public DateTimeOffset Timestamp { get; private set; } = DateTimeOffset.Now;
}
