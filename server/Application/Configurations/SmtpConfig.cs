namespace Application.Configurations
{
    public class SmtpConfig
    {
        public string Server { get; set; } = default!;
        public int Port { get; set; } = 0;
        public string Username { get; set; } = default!;
        public string Password { get; set; } = default!;
        public bool EnableSsl { get; set; } = true;
        public string FromEmail { get; set; } = default!;
    }
}
