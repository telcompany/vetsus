namespace Vetsus.Application.DTO
{
    public record PetResponse(string Name, DateTime BirthDate, string SpeciesId, string OwnerId);
}
