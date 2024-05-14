using CityReporter.Data.Entities;
using CityReporter.Models.DTOs.ReportDtos;

namespace CityReporter.Services.Extensions
{
    public static class ReportDataConversions
    {
        public static Report ConvertToEntity(this CreateReportDto report)
        {
            return new Report
            {
                Title = report.Title,
                Image = report.Image,
                Description = report.Description,
                Location = report.Location
            };
        }
        public static IEnumerable<ResponseReportWithStatusDto> ConvertToReportWithStatusDto(this IEnumerable<Report> reports)
        {
            return (from report in reports
                    select new ResponseReportWithStatusDto
                    {
                        Id = report.Id,
                        Title = report.Title,
                        Image = report.Image,
                        Description = report.Description,
                        Location = report.Location,
                        Status = report.Status.StatusTitle
                    }).ToList();
        }
        public static IEnumerable<ResponseReportDto> ConvertToDto(this IEnumerable<Report> reports)
        {
            return (from report in reports
                    select new ResponseReportDto
                    {
                        Id = report.Id,
                        Title = report.Title,
                        Image = report.Image,
                        Description = report.Description,
                        Location = report.Location
                    }).ToList();
        }

        public static ResponseReportWithStatusDto ConvertToDto(this Report report)
        {
            return new ResponseReportWithStatusDto
            {
                Id = report.Id,
                Title = report.Title,
                Image = report.Image,
                Description = report.Description,
                Location = report.Location,
                Status = report.Status.StatusTitle
            };
        }
    }
}
