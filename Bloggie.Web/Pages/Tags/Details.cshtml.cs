using Bloggie.Web.Models.Domain;
using Bloggie.Web.Repositories.Intefaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bloggie.Web.Pages.Tags
{
    public class DetailsModel : PageModel
    {
        private readonly IBlogPostRepository _blogPostRepository ;
        public List<BlogPost> Posts { get; set; }
        public DetailsModel( IBlogPostRepository blogPostRepository)
        {
                _blogPostRepository = blogPostRepository;
        }
        public async Task<IActionResult> OnGet( string tagName)
        {
            Posts = (await _blogPostRepository.GetAllAsync(tagName)).ToList();
            return Page();
        }
    }
}
