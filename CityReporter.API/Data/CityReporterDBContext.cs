using CityReporter.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityReporter.API.Data
{
    public class CityReporterDBContext:DbContext
    {
        public CityReporterDBContext(DbContextOptions<CityReporterDBContext> options):base(options)
        {

        }

        public DbSet<User> Users { get; set; }
    }
}
