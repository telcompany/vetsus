namespace Vetsus.Application.DTO
{    
    public class CreatePetRequest
    {
        public string Name { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string SpeciesId { get; set; }
        public string OwnerId { get; set; }
    }
}
