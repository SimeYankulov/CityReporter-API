using CityReporter.API.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace CityReporter.API.Data
{
    public class CityReporterDBContext:DbContext
    {
        public CityReporterDBContext(DbContextOptions<CityReporterDBContext> options):base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Status> Statuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
            modelBuilder.Entity<Status>().HasData( // Automatically add these records to the Category table
            new Status { Id = 1, StatusTitle = "Draft" },
            new Status { Id = 2, StatusTitle = "In proggress" },
            new Status { Id = 3, StatusTitle = "Approved" },
            new Status { Id = 4, StatusTitle = "Declined" }
            );
        }
    }
}
