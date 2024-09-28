namespace Vetsus.Application.DTO
{
    public record VetResponse(string Id, string FirstName, string LastName, string Phone, int Total);
    public record VetByIdResponse(string Id, string FirstName, string LastName, string Phone);
}
