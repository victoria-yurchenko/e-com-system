using Microsoft.Extensions.Options;
using Application.Configurations;
using Application.Exceptions;

namespace Application.Factories
{
    public class ExceptionFactory
    {
        private readonly ErrorMessagesConfig _errorMessages;

        public ExceptionFactory(IOptions<ErrorMessagesConfig> errorMessages)
        {
            _errorMessages = errorMessages.Value;
        }

        public UserAlreadyExistsException UserAlreadyExists()
        {
            return new UserAlreadyExistsException();
        }

        public InvalidCredentialsException InvalidCredentials()
        {
            return new InvalidCredentialsException();
        }
    }
}
