namespace Vetsus.Application.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public List<string>? Errors { get; set; }

        public UserNotFoundException(string message) : base(message) { }

        public UserNotFoundException(string message, List<string>? errors) : base(message)
        {
            Errors = errors;
        }
    }
}
