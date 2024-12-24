using Vetsus.Domain.Common;
using Vetsus.Domain.Utilities;

namespace Vetsus.Domain.Entities
{
    [TableName("Pets")]
    public class Pet: IDbEntity
    {
        [PrimaryKey]
        [ColumnName("Id")]
        public string Id { get; set; } = ShortGuid.NewGuid();

        [ColumnName("Name")]
        public string Name { get; set; } = string.Empty;

        [ColumnName("Gender")]
        public string Gender { get; set; } = string.Empty;

        [ColumnName("BirthDate")]
        public DateTime? BirthDate { get; set; }

        [ForeignKey]
        [ColumnName("SpeciesId")]
        public string SpeciesId { get; set; } = string.Empty;

        [ForeignKey]
        [ColumnName("OwnerId")]
        public string OwnerId { get; set; } = string.Empty;

        [ColumnName("Created")]
        public DateTime Created { get; set; } = DateTime.Now;

        [ColumnName("CreatedBy")]
        public string CreatedBy { get; set; } = string.Empty;
    }
}
