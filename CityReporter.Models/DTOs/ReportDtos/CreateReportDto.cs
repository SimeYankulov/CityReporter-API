using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace CityReporter.Models.DTOs.ReportDtos
{
    public class CreateReportDto
    {
        [Required]
        public string Title { get; set; }
        public IFormFile ImageFile { get; set; }
        public byte[] Image { get; set; }
        [Required]
        public string Description { get; set; } 
        [Required]
        public string Location { get; set; }

    }
}
