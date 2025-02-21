using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResumeProject.Models
{
    public class Comment
    {
        [Key]
        public string CommentId { get; set; } = Guid.NewGuid().ToString(); // Primary key

        [Required]
        public string CommentText { get; set; } = string.Empty;  // The comment content

        public DateTime CreatedAt { get; set; } = DateTime.Now;  // When the comment was created

        // Foreign key to ApplicationUser
        [Required]
        public string? ApplicationUserId { get; set; }

        // Navigation property to the ApplicationUser
        [ForeignKey(nameof(ApplicationUserId))]
        public ApplicationUser? User { get; set; }
    }
}