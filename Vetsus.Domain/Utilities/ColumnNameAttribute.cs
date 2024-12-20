namespace Vetsus.Domain.Utilities
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ColumnNameAttribute: Attribute
    {
        public string NameValue { get; set; }

        public bool IsForInsertOrUpdate { get; set; }

        public ColumnNameAttribute(string nameValue)
        {
            NameValue = nameValue;
            IsForInsertOrUpdate = true;
        }

        public ColumnNameAttribute(string nameValue, bool isForInsertOrUpdate)
        {
            NameValue = nameValue;
            IsForInsertOrUpdate = isForInsertOrUpdate;
        }
    }
}
