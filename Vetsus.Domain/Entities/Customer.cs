using Vetsus.Domain.Common;
using Vetsus.Domain.Utilities;

namespace Vetsus.Domain.Entities
{
    [TableName("Customers")]
    public class Customer: IDbEntity
    {
        [ColumnName("Id")]
        public string Id { get; set; }

        [ColumnName("FirstName")]
        public string FirstName { get; set; }
        
        [ColumnName("LastName")]
        public string LastName { get; set; }

        [ColumnName("Address")]
        public string Address { get; set; }

        [ColumnName("Phone")]
        public string Phone { get; set; }

        [ColumnName("Email")]
        public string Email { get; set; }
    }
}
