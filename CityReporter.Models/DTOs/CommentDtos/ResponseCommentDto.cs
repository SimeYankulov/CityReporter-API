﻿namespace CityReporter.Models.DTOs.CommentDtos
{
    public class ResponseCommentDto
    {
        public int userId { get; set; }
        public int reportId { get; set; }
        public DateTime postedOn { get; set; }
        public string commentContent { get; set; }
    }
}
