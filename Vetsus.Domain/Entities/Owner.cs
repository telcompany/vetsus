using Vetsus.Domain.Common;
using Vetsus.Domain.Utilities;

namespace Vetsus.Domain.Entities
{
    [TableName("Owners")]
    public class Owner: IDbEntity
    {
        [PrimaryKey]
        [ColumnName("Id")]
        public string Id { get; set; } = ShortGuid.NewGuid();

        [ColumnName("FirstName")]
        public string FirstName { get; set; } = string.Empty;

        [ColumnName("LastName")]
        public string LastName { get; set; } = string.Empty;

        [ColumnName("Address")]
        public string Address { get; set; } = string.Empty;

        [ColumnName("Phone")]
        public string Phone { get; set; } = string.Empty;

        [ColumnName("Email")]
        public string Email { get; set; } = string.Empty;

        [ColumnName("Total", false)]
        public int Total { get; set; } = 0;

        [ColumnName("Created")]
        public DateTime Created { get; set; } = DateTime.Now;

        [ColumnName("CreatedBy")]
        public string CreatedBy { get; set; } = string.Empty;
    }
}
