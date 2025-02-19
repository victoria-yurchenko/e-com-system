using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Infrastructure.DatabaseContext
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer("Server=sqlserver,1433;Database=SubscriptionDb;User Id=sa;Password=Strong@Passw0rd;TrustServerCertificate=true;");

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
