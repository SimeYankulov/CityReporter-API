using CityReporter.Models.DTOs.CommentDtos;

namespace CityReporter.Services.Services.Contracts
{
    public interface ICommentService
    {
        Task<PostResponseCommentDto> PostComment(ResponseCommentDto comment);
        Task<bool> DeleteComment(int id);
        Task<IEnumerable<ResponseCommentDto>> GetComments(int reportId);
    }
}
