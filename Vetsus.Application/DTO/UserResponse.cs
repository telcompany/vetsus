namespace Vetsus.Application.DTO
{
    public record UserResponse(string Id, string Email, string Username, string Role, DateTime Created, string CreatedBy, int Total);
    public record UserByIdResponse(string Id, string Email, string Username, string Role);
}
