using CityReporter.API.Entities;
using CityReporter.Models.DTOs.ReportDtos;

namespace CityReporter.API.Repositories.Contracts
{
    public interface IReportRepository
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
