namespace CityReporter.Data.Entities
{
    public class Status
    { 
        public int Id { get; set; }
        public string StatusTitle { get; set; } = String.Empty;

        public List<Report> Reports { get; set; } = new List<Report>();
    }
}
