namespace Vetsus.Application.DTO
{
    public record PetResponse(string Name, DateTime? BirthDate, string SpeciesId, string OwnerId);

    public record GetPetsByOwnerIdResponse(string PetId, string Name, string Gender, string Species, string BirthDate);
}
