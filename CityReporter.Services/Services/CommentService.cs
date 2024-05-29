using CityReporter.Data.Repositories.Contracts;
using CityReporter.Models.DTOs.CommentDtos;
using CityReporter.Services.Extensions;
using CityReporter.Services.Services.Contracts;

namespace CityReporter.Services.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository commentRepository;

        public CommentService(ICommentRepository commentRepository)
        {
            this.commentRepository = commentRepository;
        }
        public async Task<bool> DeleteComment(int id)
        {
            return await this.commentRepository.DeleteComment(id);
        }

        public async Task<IEnumerable<ResponseCommentDto>> GetComments(int reportId)
        {
            var comments = await this.commentRepository.GetItems(reportId);

            return comments.ConvertToDto();
        }

        public async Task<PostResponseCommentDto> PostComment(ResponseCommentDto comment)
        {
            var newComment = await this.commentRepository.AddComment(comment.ConvertToEntity());

            return newComment.ConvertToDto();
        }
    }
}
