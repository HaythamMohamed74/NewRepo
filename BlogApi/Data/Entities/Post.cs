using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BlogApi.Data.Entities
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        [MaxLength(50)]
        public string AuthorName { get; set; }
       
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [DefaultValue(false)]
        public bool IsDeleted { get; set; }
    }
}
