namespace Application.Contracts.DataTransferObjects.ShelfDto;

public static class Dto
{
    public record Location(string Session, int Hall, int Bookcase, int Rack);
}