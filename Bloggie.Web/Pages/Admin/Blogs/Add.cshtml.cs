using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Bloggie.Web.Models.ViewModels;
using Bloggie.Web.Repositories;
using Bloggie.Web.Repositories.Intefaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text.Json;


namespace Bloggie.Web.Pages.Admin.Blogs
{
    [Authorize(Roles ="Admin")]
    public class AddModel : PageModel
    {
        
        private readonly IBlogPostRepository _blogpostRepository;
        [BindProperty]
        public AddBlogPost AddBlogPostRequest { get; set; } 
        [BindProperty]
        public IFormFile? FeaturedImage { get; set; }
        
        [BindProperty]
        [Required]
        public string Tags { get; set; }
        public AddModel(IBlogPostRepository blogPostRepository)
        {
            
            _blogpostRepository = blogPostRepository;
        }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost() 
        {
            ValidateAddBlogPost();
            if (ModelState.IsValid)
            {
                var blogPost = new BlogPost()
                {
                    Heading = AddBlogPostRequest.Heading,
                    PageTitle = AddBlogPostRequest.PageTitle,
                    Content = AddBlogPostRequest.Content,
                    ShortDescription = AddBlogPostRequest.ShortDescription,
                    FeaturedImageUrl = AddBlogPostRequest.FeaturedImageUrl,
                    UrlHandle = AddBlogPostRequest.UrlHandle,
                    PublishedDate = AddBlogPostRequest.PublishedDate,
                    Author = AddBlogPostRequest.Author,
                    Visible = AddBlogPostRequest.Visible,
                    Tags = new List<Tag>(Tags.Split(',').Select(x => new Tag() { Name = x.Trim() }))

                };
                await _blogpostRepository.AddAsync(blogPost);

                var notification = new Notification
                {
                    Message = "Novo Post criado com sucesso!",
                    Type = Enums.NotificationType.Success
                };
                TempData["Notification"] = JsonSerializer.Serialize(notification);

                return RedirectToPage("/Admin/Blogs/List");
            }

            return Page();
            
        }
        private void ValidateAddBlogPost()
        {
           
            if (AddBlogPostRequest.PublishedDate.Date < DateTime.Now.Date) 
            {
                ModelState.AddModelError("AddBlogPostRequest.PublishedDate", 
                    $"PublishedDate não pode ser menor que a data atual! ");
            }
        }
    }
    
}
