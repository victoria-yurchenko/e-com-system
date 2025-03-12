namespace Application.Configurations
{
    public class SmsConfig
    {
        public required string AccountSid { get; set; }
        public required string AuthToken { get; set; }
        public required string FromNumber { get; set; }
        public Dictionary<string, string> Templates { get; set; } = new();
    }
}
