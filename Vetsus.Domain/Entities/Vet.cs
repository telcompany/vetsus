using Vetsus.Domain.Common;
using Vetsus.Domain.Utilities;

namespace Vetsus.Domain.Entities
{
    [TableName("Vets")]
    public class Vet: IDbEntity
    {
        [PrimaryKey]
        [ColumnName("Id")]
        public string Id { get; set; } = ShortGuid.NewGuid();

        [ColumnName("FirstName")]
        public string FirstName { get; set; } = string.Empty;

        [ColumnName("LastName")]
        public string LastName { get; set; } = string.Empty;

        [ColumnName("Phone")]
        public string Phone { get; set; } = string.Empty;
    }
}
