namespace Vetsus.Application.DTO
{
    public record ChangePasswordRequest(string Id, string CurrentPassword, string NewPassword);

}
