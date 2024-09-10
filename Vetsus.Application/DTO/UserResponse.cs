namespace Vetsus.Application.DTO
{
    public record UserResponse(string Id, string Email, string Username, string Role, int Total);
    public record UserByIdResponse(string Id, string Email, string Username, string Role);
}
