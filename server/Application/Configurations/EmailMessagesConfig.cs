namespace Application.Configurations
{
    public class EmailMessagesConfig
    {
        public Dictionary<string, EmailTemplateConfig> Templates { get; set; } = new();
    }
}