namespace Vetsus.Application.Exceptions
{
    public class NotFoundException: Exception
    {
        public List<string>? Errors { get; set; }

        public NotFoundException(string message) : base(message) { }

        public NotFoundException(string message, List<string>? errors) : base(message)
        {
            Errors = errors;
        }
    }
}
