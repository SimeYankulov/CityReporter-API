using CityReporter.Models.DTOs.CommentDtos;
using CityReporter.Services.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CityReporter.API.Controllers
{
    [Route("comments")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService commentService;
        private readonly ILogger<CommentController> _logger;

        public CommentController(ICommentService commentService, ILogger<CommentController> logger)
        {
            this.commentService = commentService;
            this._logger = logger;
        }

        [HttpPost("/comment")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<PostResponseCommentDto>> PostComment([FromBody]ResponseCommentDto comment)
        {
            try
            {
                if(comment == null)
                {
                    return BadRequest();
                }
                var result = await this.commentService.PostComment(comment);

                if (result.commentContent != null) return Ok(result);
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(StatusCodes.Status400BadRequest,
                 "Error writing data in the database : " + ex.Message);

            }
        }
        [HttpGet("/comments/report/{id:int}")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<IEnumerable<ResponseCommentDto>>> GetItems(int reportId)
        {
            try
            {
                var comments = await this.commentService.GetComments(reportId);

                if(comments.Count() > 0)
                {
                    return Ok(comments);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                
                return StatusCode(StatusCodes.Status400BadRequest,
                "Error retriving data from the database : " + ex.Message);
            }
        }

        [HttpDelete("/comment")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<bool>> DeleteItem(int commentId)
        {
            try
            {
                var result = await this.commentService.DeleteComment(commentId);

                if (result)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());

                return StatusCode(StatusCodes.Status400BadRequest,
                   "Error deleting data in the database:" + ex.Message);
            }
        }
    }
}
