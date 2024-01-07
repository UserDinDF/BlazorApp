using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BlazorAppServer;
using BlazorAppServer.Data;
using BlazorAppServer.Data.Models;

namespace BlazorAppServer.Data.Models
{
    [Table("Images", Schema = "data")]
    public class ImageModel
    {
        [Key]
        public int Id { get; set; }
        public string ImageURL { get; set; }
        public string Description { get; set; }
        public int FileModelId { get; set; }
        public FileModel FileModel { get; set; }
    }
}
