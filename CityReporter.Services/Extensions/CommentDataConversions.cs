using CityReporter.Data.Entities;
using CityReporter.Models.DTOs.CommentDtos;

namespace CityReporter.Services.Extensions
{
    public static class CommentDataConversions
    {
        public static IEnumerable<ResponseCommentDto> ConvertToDto(this IEnumerable<Comment> comments)
        {
            return (from comment in comments
                    select new ResponseCommentDto
                    {
                        userId= comment.UserId,
                        reportId = comment.ReportId,
                        postedOn = comment.PostedOn,
                        commentContent = comment.CommentContent
                    }

                ).ToList();
        }
        public static Comment ConvertToEntity(this ResponseCommentDto comment)
        {
            return new Comment
            {
                UserId = comment.userId,
                CommentContent = comment.commentContent,
                ReportId = comment.reportId,
                PostedOn = comment.postedOn
            };
        }

        public static PostResponseCommentDto ConvertToDto(this Comment comment)
        {
            return new PostResponseCommentDto
            {
                userName = comment.User.Name,
                commentContent = comment.CommentContent,
                postedOn = comment.PostedOn
            };
        }
    }
}
