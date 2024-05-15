
using CityReporter.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityReporter.Data.Data
{
    public class CityReporterDBContext:DbContext
    {
        public CityReporterDBContext():base()
        {

        }
        public CityReporterDBContext(DbContextOptions<CityReporterDBContext> options):base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Status> Statuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
            modelBuilder.Entity<Status>().HasData( 
            new Status { Id = 1, StatusTitle = "Draft" },
            new Status { Id = 2, StatusTitle = "In proggress" },
            new Status { Id = 3, StatusTitle = "Approved" },
            new Status { Id = 4, StatusTitle = "Declined" }
            );
        }
    }
}
