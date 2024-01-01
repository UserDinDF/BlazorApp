using BlazorAppServer.Pages;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static MudBlazor.CategoryTypes;
using BlazorAppServer.Enums;

namespace BlazorAppServer.Data
{
    [Table("Files", Schema = "data")]
    public class FileModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Название файла обязательна")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Краткое описание обязательно")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Полное описание обязательно")]
        public string DescriptionFull { get; set; }

        [Required(ErrorMessage = "Выберите категорию")]
        public string Category { get; set; }
     
        public DateTime CreationTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public string DownloadUrl { get; set; }
        public string Image { get; set; }
        public string Language { get; set; }

        [Required(ErrorMessage = "Название страницы обязательно")]
        [Localizable(false)]
        public string Seo_Title { get; set; }
        [Required(ErrorMessage = "Краткое описание обязательно")]
        [Localizable(false)]
        public string Seo_Description { get; set; }
        [Required(ErrorMessage = "Ключевые слова обязательно")]
        public string Seo_Keywords { get; set; }

        [Required(ErrorMessage = "SEO Url обязательно")]
        public string Seo_Url { get; set; }
        public string Keywords { get; set; }
        public int Views { get; set; }
        public int Downloads { get; set; }
        public int Comments { get; set; }
        public float Rating { get; set; }

        public string Price { get; set; }
        public string Licence { get; set; }
        public string Developer { get; set; }
        public string Version { get; set; }
        public string OC { get; set; }
        public string DownloadSize { get; set; }
        public string DownloadFileName { get; set; }
        public string GetCategoryLink()
        { 
            var categoryName = Enums.EnumHelper.GetCategoryNameFromSlug(Category);

            return $"/load/{categoryName}/{Seo_Url}";
        }

        public string GetFilePath()
        {
            return $"/load/{Id}/{Seo_Url}";
        }
    }
}
