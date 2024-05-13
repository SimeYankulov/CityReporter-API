using System.Diagnostics.CodeAnalysis;

namespace CityReporter.API.Entities
{
    public class Status
    { 
        public int Id { get; set; }
        public string StatusTitle { get; set; }

        public List<Report> Reports { get; set; } = new List<Report>();
    }
}
