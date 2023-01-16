using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Blogovic.ViewModels.Article.Create
{
    public class CreateViewModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        [Display(Name = "Article Picture")]
        public IFormFile ArticlePicture { get; set; }
    }
}
