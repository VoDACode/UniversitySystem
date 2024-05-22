namespace University.Domain.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string message) : base(message)
        {
        }

        public BadRequestException() : base("400 Bad Request")
        {
        }
    }
}
