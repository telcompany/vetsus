using Vetsus.Domain.Common;
using Vetsus.Domain.Utilities;

namespace Vetsus.Domain.Entities
{
    [TableName("Specialties")]
    public class Specialty: IDbEntity
    {
        [PrimaryKey]
        [ColumnName("Id")]
        public string Id { get; set; } = ShortGuid.NewGuid();

        [ColumnName("Name")]
        public string Name { get; set; } = string.Empty;
    }
}
