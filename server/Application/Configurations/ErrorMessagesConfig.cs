namespace Application.Configurations
{
    public class ErrorMessagesConfig
    {
        public string EmailAlreadyInUse { get; set; } = "";
        public string InvalidCredentials { get; set; } = "";
        public string UserNotFound { get; set; } = "";
        public string PasswordTooWeak { get; set; } = "";
        public string Unknown { get; set; } = "";
    }
}
