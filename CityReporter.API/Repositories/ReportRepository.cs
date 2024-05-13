using CityReporter.API.Data;
using CityReporter.API.Extensions;
using CityReporter.API.Repositories.Contracts;
using CityReporter.Models.DTOs.ReportDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CityReporter.API.Repositories
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

        public async Task<ResponseReportWithStatusDto> GetItem(int id)
        {
            var report = await this.cityReporterDBContext.Reports
                                                .Include(r => r.Status)
                                                .SingleOrDefaultAsync(r => r.Id == id);

            if (report != null)
            {
                return report.ConvertToDto();
            }
            else return new ResponseReportWithStatusDto(); 
        }

        public async Task<IEnumerable<ResponseReportDto>> GetItems()
        {
            var reports = await this.cityReporterDBContext.Reports.ToListAsync();

            return reports.ConvertToDto();
        }

        public async Task<IEnumerable<ResponseReportWithStatusDto>> GetItemsStatus(int id)
        {
            var items = await this.cityReporterDBContext.Reports
                                                        .Include(r => r.Status)
                                                        .Where(r => r.StatusId == id)
                                                        .ToListAsync();

            return items.ConvertToReportWithStatusDto();
        }

        public async Task<bool> PostReport(CreateReportDto report)
        {
            
            if (report.Title != null)
            {
                var result = await this.cityReporterDBContext.Reports.AddAsync(report.ConvertToEntity());
           
     

            var newReport =await this.cityReporterDBContext.Reports.FindAsync(result.Entity.Id);

            if(newReport != null)
            {
                return true;
            }
            }
            return false;
        }

        public async Task<bool> PutItem(int id, CreateReportDto report)
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
            await this.cityReporterDBContext.SaveChangesAsync();

            return true;

        }
    }
}
