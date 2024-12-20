namespace Vetsus.Application.DTO
{
    public record OwnerResponse(string FirstName, string LastName, string Address, string Phone, string Email, int Total = 0);
}
