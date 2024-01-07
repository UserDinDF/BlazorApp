using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorAppServer.Data
{
    [Table("Comments", Schema = "data")]
    public class CommentModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Author is required")]
        public string Author { get; set; }

        [Required(ErrorMessage = "Content is required")]
        public string Content { get; set; }

        [Required(ErrorMessage = "Timestamp is required")]
        public DateTime Timestamp { get; set; }

        public int FileModelId { get; set; }

        public FileModel FileModel { get; set; }
    }
}
