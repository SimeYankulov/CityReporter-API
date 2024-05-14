using CityReporter.Data.Entities;

namespace CityReporter.Data.Repositories.Contracts
{
    public interface IReportRepository
    {
        Task<bool> PostReport(Report report);
        Task<IEnumerable<Report>> GetItems();
        Task<IEnumerable<Report>> GetItemsStatus(int id);
        Task<Report> GetItem(int id);
        Task<bool> PutItem(int id, Report report);
        Task<bool> PutStatus(int reportId, int statusId);
        Task<bool> DeleteItem(int id);
    }
}
