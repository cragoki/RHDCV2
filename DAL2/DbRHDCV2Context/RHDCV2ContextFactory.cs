using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DAL.DbRHDCV2Context
{
    public class RHDCV2ContextFactory : IDesignTimeDbContextFactory<RHDCV2Context>
    {
        public RHDCV2Context CreateDbContext(string[] args)
        {
            // Build configuration
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) 
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<RHDCV2Context>();
            var connectionString = configuration.GetConnectionString("SQLServer");

            // Configure DbContext with the connection string
            optionsBuilder.UseSqlServer(connectionString);

            return new RHDCV2Context(optionsBuilder.Options);
        }
    }
}
