using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace CityReporter.API.Entities
{
    public class Report
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public byte[] Image { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public int? StatusId { get; set; }
        [ForeignKey("StatusId")]
        public Status? Status { get; set; }  
    }
}
