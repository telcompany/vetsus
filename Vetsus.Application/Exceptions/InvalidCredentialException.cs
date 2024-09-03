namespace Vetsus.Application.Exceptions
{
    public class InvalidCredentialException : Exception
    {
        public List<string>? Errors { get; set; }

        public InvalidCredentialException(string message) : base(message) { }

        public InvalidCredentialException(string message, List<string>? errors) : base(message)
        {
            Errors = errors;
        }
    }
}
