using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vetsus.Domain.Common;
using Vetsus.Domain.Utilities;

namespace Vetsus.Domain.Entities
{
    [TableName("Permissions")]
    public sealed class Permission : IDbEntity
    {
        [PrimaryKey]
        [ColumnName("Id")]
        public string Id { get; set; } = ShortGuid.NewGuid();

        [ColumnName("Name")]
        public string Name { get; set; } = string.Empty;
    }
}
