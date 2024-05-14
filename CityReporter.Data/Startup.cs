using CityReporter.Data.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CityReporter.Data
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IConfiguration config, IServiceCollection services)
        {

            services.AddDbContext<CityReporterDBContext>(options =>
            options.UseSqlServer(config.GetConnectionString("CityReporterConnection"))); 
        }

    }
}