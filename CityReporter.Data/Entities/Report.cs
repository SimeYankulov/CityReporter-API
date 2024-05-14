using System.ComponentModel.DataAnnotations.Schema;

namespace CityReporter.Data.Entities
{
    public class Report
    {
        public int Id { get; set; }
        public string Title { get; set; } = String.Empty;
        public byte[] Image { get; set; } = Array.Empty<byte>();
        public string Description { get; set; } = String.Empty;
        public string Location { get; set; } = String.Empty;
        public int? StatusId { get; set; }
        [ForeignKey("StatusId")]
        public Status? Status { get; set; }  
    }
}
