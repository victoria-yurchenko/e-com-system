using Application.Configurations;

namespace Application.Exceptions
{
    public class UserAlreadyExistsException : Exception
    {
        private static readonly string DefaultMessage = new ErrorMessagesConfig().EmailAlreadyInUse;

        public UserAlreadyExistsException() : base(DefaultMessage) {}

        public UserAlreadyExistsException(string message) : base(message) {} 
    }
}
