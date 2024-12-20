namespace Vetsus.Application.DTO
{
    public record UpdateUserRequest(string Id, string FirstName, string LastName, string Username, string Role);
}
