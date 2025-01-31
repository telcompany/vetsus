namespace Vetsus.Application.DTO
{
    public record OwnerResponse(string FirstName, string LastName, string Address, string Phone, string Email, int Total = 0);

    public record GetOwnerResponse(string Id, string FirstName, string LastName, string Phone, DateTime Created, string CreatedBy, int TotalPets, int Total = 0);
}
