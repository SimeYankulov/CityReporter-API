using CityReporter.Services.Contracts;
using CityReporter.Models.DTOs.ReportDtos;
using CityReporter.Services.Extensions;
using CityReporter.Data.Repositories.Contracts;

namespace CityReporter.Services
{
    public class ReportsService : IReportsService
    {
        private readonly IReportRepository reportRepository;

        public ReportsService(IReportRepository reportRepository)
        {
            this.reportRepository = reportRepository;
        }
        public async Task<bool> DeleteItem(int id)
        {
            return await this.reportRepository.DeleteItem(id);
        }

        public async Task<ResponseReportWithStatusDto> GetItem(int id)
        {
            var report = await this.reportRepository.GetItem(id);

            return report.ConvertToDto();
        }

        public async Task<IEnumerable<ResponseReportDto>> GetItems()
        {
            var reports = await this.reportRepository.GetItems();

            return reports.ConvertToDto();
        }

        public async Task<IEnumerable<ResponseReportWithStatusDto>> GetItemsStatus(int id)
        {
            var reports = await this.reportRepository.GetItemsStatus(id);

            return reports.ConvertToReportWithStatusDto();
        }

        public async Task<bool> PostReport(CreateReportDto report)
        {
            return await this.reportRepository.PostReport(report.ConvertToEntity());
        }

        public async Task<bool> PutItem(int id, CreateReportDto report)
        {
            return await this.reportRepository.PutItem(id, report.ConvertToEntity());
        }

        public async Task<bool> PutStatus(int reportId, int statusId)
        {
            return await this.reportRepository.PutStatus(reportId, statusId);
        }
    }
}
