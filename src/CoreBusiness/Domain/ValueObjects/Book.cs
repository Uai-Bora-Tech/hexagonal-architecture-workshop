using Domain.ValueTypes;

namespace Domain.ValueObjects;

public record Book(
    string Name,
    string Description,
    string Author,
    Language Language,
    int Pages,
    DateTime PublicationAt,
    string PublishingCompany);