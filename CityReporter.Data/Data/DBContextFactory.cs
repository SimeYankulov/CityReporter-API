using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace CityReporter.Data.Data
{
    internal class DBContextFactory : IDesignTimeDbContextFactory<CityReporterDBContext>
    {
        public CityReporterDBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CityReporterDBContext>();
            
            var configuration = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile("appsettings.json")
                                .Build();

            var connectionString = configuration.GetConnectionString("CityReporterConnection");
            optionsBuilder.UseSqlServer(connectionString);

            return new CityReporterDBContext(optionsBuilder.Options);

        }

    }
}
