using CityReporter.Models.DTOs.ReportDtos;

namespace CityReporter.Services.Contracts
{
    public interface IReportsService
    {
        Task<bool> PostReport(CreateReportDto report);
        Task<IEnumerable<ResponseReportDto>> GetItems();
        Task<IEnumerable<ResponseReportWithStatusDto>> GetItemsStatus(int id);
        Task<ResponseReportWithStatusDto> GetItem(int id);
        Task<bool> PutItem(int id, CreateReportDto report);
        Task<bool> PutStatus(int reportId, int statusId);
        Task<bool> DeleteItem(int id);
    }
}
