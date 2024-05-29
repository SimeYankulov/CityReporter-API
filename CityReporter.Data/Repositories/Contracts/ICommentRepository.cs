using CityReporter.Data.Entities;

namespace CityReporter.Data.Repositories.Contracts
{
    public interface ICommentRepository
    {
        Task<IEnumerable<Comment>> GetItems(int reportId);
        Task<Comment> AddComment(Comment comment);
        Task<bool> DeleteComment(int commentId);
    }
}
