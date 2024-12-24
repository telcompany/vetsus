namespace Vetsus.Application.DTO
{
    public record OwnerResponse(string FirstName, string LastName, string Address, string Phone, string Email, int Total = 0);

    public record GetOwnerResponse(string FirstName, string LastName, string Address, string Phone, string Email, DateTime Created, string CreatedBy, int Total = 0);
}
