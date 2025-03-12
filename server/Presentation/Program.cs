using Presentation.Configuration;

namespace Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = ConfigManager.CreateBuilder(args);
            var app = builder.Build();
            ConfigManager.ConfigureApplication(app);
            app.Run();
        }
    }
}
