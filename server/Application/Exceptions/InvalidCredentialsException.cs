using Application.Configurations;

namespace Application.Exceptions
{
    public class InvalidCredentialsException : Exception
    {
        private static readonly string DefaultMessage = new ErrorMessagesConfig().InvalidCredentials;

        public InvalidCredentialsException() : base(DefaultMessage) {}

        public InvalidCredentialsException(string message) : base(message) {} 
    }
}
