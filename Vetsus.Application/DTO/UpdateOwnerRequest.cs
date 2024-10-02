namespace Vetsus.Application.DTO
{
    public record UpdateOwnerRequest(string Id, string FirstName, string LastName, string Address, string Phone, string Email);
}
