namespace Vetsus.Domain.Utilities
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ColumnNameAttribute: Attribute
    {
        public string NameValue { get; set; }

        public ColumnNameAttribute(string nameValue)
        {
            NameValue = nameValue;
        }
    }
}
