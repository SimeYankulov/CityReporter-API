namespace CityReporter.Models.DTOs.ReportDtos
{
    public class ResponseReportDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public byte[] Image { get; set; }
    }
}
