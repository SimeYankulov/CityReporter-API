namespace CityReporter.Models.DTOs.CommentDtos
{
    public class ResponseCommentDto
    {
        public int userId { get; set; }
        public int commentId { get; set; }
        public DateTime postedOn { get; set; }
        public string commentText { get; set; }
    }
}
