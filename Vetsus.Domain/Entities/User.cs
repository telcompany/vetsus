using Vetsus.Domain.Common;
using Vetsus.Domain.Utilities;

namespace Vetsus.Domain.Entities
{
    [TableName("Users")]
    public class User : IDbEntity
    {
        [PrimaryKey]
        [ColumnName("Id")]
        public string Id { get; set; } = ShortGuid.NewGuid();

        [ColumnName("UserName")]
        public string UserName { get; set; } = string.Empty;

        [ColumnName("FirstName")]
        public string FirstName { get; set; } = string.Empty;

        [ColumnName("LastName")]
        public string LastName { get; set; } = string.Empty;

        [DistinguishingUniqueKey]
        [ColumnName("Email")]
        public string Email { get; set; } = string.Empty;

        [ColumnName("PasswordHash")]
        public string PasswordHash { get; set; } = string.Empty;

        [ColumnName("Created")]
        public DateTime Created { get; set; } = DateTime.Now;

        [ColumnName("CreatedBy")]
        public string CreatedBy { get; set; } = string.Empty;
    }
}
