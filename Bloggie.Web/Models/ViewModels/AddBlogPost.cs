using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Bloggie.Web.Models.ViewModels
{
    public class AddBlogPost
    {
        [Required]
        [MaxLength(200)]
        public string Heading { get; set; }
        [Required]
        [MaxLength(200)]
        public string PageTitle { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        [MaxLength(500)]
        public string ShortDescription { get; set; }
        [Required]
        [MaxLength(255)]
        public string FeaturedImageUrl { get; set; }
        [Required]
        [MaxLength(255)]
        public string UrlHandle { get; set; }
        [Required]
        public DateTime PublishedDate { get; set; }
        [Required]
        [MaxLength(150)]
        public string Author { get; set; }
       
        public bool Visible { get; set; }
    }
}
