using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResumeProject.Models
{
    public class UserComment
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString(); // Primary key for this join entity

        // Foreign key to ApplicationUser
        [Required]
        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; }

        // Foreign key to Comment
        [Required]
        public string CommentId { get; set; }

        [ForeignKey(nameof(CommentId))]
        public Comment Comment { get; set; }
    }
}