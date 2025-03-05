using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResumeProject.Models
{
    public class Comment
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString(); // Primary key

        [Required]
        public string CommentText { get; set; } = string.Empty;  // The comment content

        public DateTime CreatedAt { get; set; } = DateTime.Now;  // When the comment was created
    }
}