namespace Application.Configurations
{
    public class ErrorMessagesConfig
    {
        public string EmailAlreadyInUse { get; set; } = default!;
        public string InvalidCredentials { get; set; } = default!;
        public string UserNotFound { get; set; } = default!;
        public string PasswordTooWeak { get; set; } = default!;
        public string Unknown { get; set; } = default!;
    }
}
