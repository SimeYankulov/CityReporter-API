using CityReporter.Data.Data;
using CityReporter.Data.Entities;
using CityReporter.Data.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace CityReporter.Data.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly CityReporterDBContext cityReporterDBContext;

        public CommentRepository(CityReporterDBContext cityReporterDBContext)
        {
            this.cityReporterDBContext = cityReporterDBContext;
        }
        public async Task<Comment> AddComment(Comment comment)
        {
            var result = await this.cityReporterDBContext.Comments.AddAsync(comment); 
            
            if(result.Entity != null)
            {
                await this.cityReporterDBContext.SaveChangesAsync();

                var newComment = await this.cityReporterDBContext.Comments.Include(c => c.User)
                                    .SingleOrDefaultAsync(c => c.Id == result.Entity.Id);

                return newComment;
            }
            else
            {
                return new Comment();
            }
        }

        public async Task<bool> DeleteComment(int commentId)
        {
            var comment = this.cityReporterDBContext.Comments.FindAsync(commentId);

            if (comment.Result != null)
            {
                this.cityReporterDBContext.Remove(comment);
                await this.cityReporterDBContext.SaveChangesAsync();
                return true;
            }
            else return false;
        }

        public async Task<IEnumerable<Comment>> GetItems(int reportId)
        {
            var comments = await this.cityReporterDBContext.Comments
                                                            .Where(c => c.ReportId == reportId)
                                                            .ToListAsync();
            return comments;

        }
    }
}
