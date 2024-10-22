namespace Vetsus.Application.DTO
{
    public record CreateOwnerRequest(string FirstName, string LastName, string Address, string Phone, string Email);


    public class CreateOwnerAndPetRequest
    {
        public CreateOwnerRequest OwnerRequest { get; set; }
        public CreatePetRequest PetRequest { get; set; }
    }


}
