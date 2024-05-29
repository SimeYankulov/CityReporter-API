using CityReporter.Data.Data;
using CityReporter.Data.Entities;
using CityReporter.Data.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace CityReporter.Data.Repositories
{
    public class ReportRepository : IReportRepository
    {
        private readonly CityReporterDBContext cityReporterDBContext;

        public ReportRepository(CityReporterDBContext cityReporterDBContext)
        {
            this.cityReporterDBContext = cityReporterDBContext;
        }
        public async Task<bool> DeleteItem(int id)
        {
            var report = this.cityReporterDBContext.Reports.FindAsync(id);

            if(report.Result != null)
            {
                this.cityReporterDBContext.Remove(report);
                await this.cityReporterDBContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Report> GetItem(int id)
        {
            var report = await this.cityReporterDBContext.Reports
                                                .Include(r => r.Status)
                                                .SingleOrDefaultAsync(r => r.Id == id);

            if (report != null)
            {
                return report;
            }
            else return new Report(); 
        }

        public async Task<IEnumerable<Report>> GetItems()
        {
            var reports = await this.cityReporterDBContext.Reports.ToListAsync();

            return reports;
        }

        public async Task<IEnumerable<Report>> GetItemsStatus(int id)
        {
            var items = await this.cityReporterDBContext.Reports
                                                        .Include(r => r.Status)
                                                        .Where(r => r.StatusId == id)
                                                        .ToListAsync();

            return items;
        }

        public async Task<bool> PostReport(Report report)
        {
            
            if (report.Title != null)
            { 
                var result = await this.cityReporterDBContext.Reports.AddAsync(report);

                await this.cityReporterDBContext.SaveChangesAsync();

                var newReport =await this.cityReporterDBContext.Reports.FindAsync(result.Entity.Id);

       

            if(newReport != null)
            {
                return true;
            }
            }
            return false;
        }

        public async Task<bool> PutItem(int id, Report report)
        {
            var reportToUpdate = await this.cityReporterDBContext.Reports.FindAsync(id);

            if (reportToUpdate != null)
            {
                reportToUpdate.Title = report.Title;
                reportToUpdate.Description = report.Description;
                reportToUpdate.Location = report.Location;
                reportToUpdate.Image = report.Image;

                var result = this.cityReporterDBContext.Reports.Update(reportToUpdate);
                await this.cityReporterDBContext.SaveChangesAsync();

                if (result != null)
                {
                    return true;
                }
                else return false;

            }
            else return false;


        }

        public async Task<bool> PutStatus(int reportId, int statusId)
        {
            var reportToUpdate = await this.cityReporterDBContext.Reports.FindAsync(reportId);

            if(reportToUpdate == null)
            {
                return false;
            }

            reportToUpdate.StatusId = statusId;
            this.cityReporterDBContext.Update(reportToUpdate); 
            await this.cityReporterDBContext.SaveChangesAsync();

            return true;

        }
    }
}
