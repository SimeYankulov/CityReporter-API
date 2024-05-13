using CityReporter.API.Repositories.Contracts;
using CityReporter.Models.DTOs.ReportDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CityReporter.API.Controllers
{
    [Route("reports")]
    public class ReportController : ControllerBase
    {
        private readonly IReportRepository reportRepository;

        public ReportController(IReportRepository reportRepository)
        {
            this.reportRepository = reportRepository;
        }

        [HttpPost("/report")]
        //[Authorize(Roles = "User")]
        [AllowAnonymous]
        public async Task<ActionResult<bool>> PostReport([FromBody]CreateReportDto report)
        {
            try
            {
                if(report == null)
                {
                    Console.WriteLine();
                }
                var result = await this.reportRepository.PostReport(report);

                if(result) return Ok(result);
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status400BadRequest,
               "Error writing data in the database : " + ex.Message);
            }
        }
        [HttpGet]
        //[Authorize(Roles = "Guest")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ResponseReportDto>>> GetReports()
        {
            try
            {
                var reports = await this.reportRepository.GetItems();

                if(reports.Count() > 0)
                {
                    return Ok(reports);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status400BadRequest,
               "Error retriving data from the database : " + ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        //[Authorize(Roles = "Guest")]
        [AllowAnonymous]
        public async Task<ActionResult<ResponseReportWithStatusDto>> GetReport(int id)
        {
            try
            {
                var reports = await this.reportRepository.GetItems();

                if(reports != null)
                {
                    return Ok(reports);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status400BadRequest,
               "Error retriving data from the database : " + ex.Message);
            }
        }

        [HttpGet("status/{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ResponseReportWithStatusDto>> GetReportStatus(int id)
        {
            try
            {
                var report = await this.reportRepository.GetItemsStatus(id);

                if(report != null)
                {
                    return Ok(report);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status400BadRequest,
               "Error retriving data from the database : " + ex.Message);
            }
        }

        [HttpPut("/report/{id:int}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<bool>> PutReport(int id, [FromBody]CreateReportDto report)
        {
            try
            {
                var result = await this.reportRepository.PutItem(id, report);

                if (result)
                {
                    return Ok(result);
                }
                else { return NotFound(); }
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPut("/report/status")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<bool>> PutReportStatus(int id, int statusId)
        {
            try
            {
                var result = await this.reportRepository.PutStatus(id,statusId);

                if (result)
                {
                    return Ok(result);
                }
                else { return NotFound(); }
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status400BadRequest,
               "Error writing data in the database : " + ex.Message);
            }
        }
        [HttpDelete("/report/{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<bool>> DeleteReport(int id)
        {
            try
            {
                var result = await this.reportRepository.DeleteItem(id);

                if (result)
                {
                    return Ok(result);
                }
                else return BadRequest();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
