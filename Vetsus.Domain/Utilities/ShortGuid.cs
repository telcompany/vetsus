namespace Vetsus.Domain.Utilities
{
    public sealed class ShortGuid
    {
        public static string NewGuid()
        {
            var guid = Guid.NewGuid();

            byte[] bytes = guid.ToByteArray();

            string base64 = Convert.ToBase64String(bytes)
                .Replace("/", "_")
                .Replace("+", "-")
                .Substring(0,22);

            return base64;
        }
    }
}
