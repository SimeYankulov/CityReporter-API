using System.ComponentModel.DataAnnotations.Schema;

namespace CityReporter.Data.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public string CommentContent { get; set; }
        public DateTime PostedOn { get; set; }
        public int ReportId { get; set; }
        [ForeignKey("ReportId")]
        public Report Report { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }


    }
}
