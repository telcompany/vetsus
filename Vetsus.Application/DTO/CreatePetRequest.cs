namespace Vetsus.Application.DTO
{
    public record CreatePetRequest(string Name, DateTime BirthDate, string SpeciesId, string OwnerId);

}
