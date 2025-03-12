namespace Application.Configurations
{
    public class SmtpConfig
    {
        public string Server { get; set; } = "";
        public int Port { get; set; } = 0;
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
        public bool EnableSsl { get; set; } = true;
        public string FromEmail { get; set; } = "";
    }
}
