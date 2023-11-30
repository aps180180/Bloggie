using Bloggie.Web.Data;
using Bloggie.Web.Enums;
using Bloggie.Web.Models.Domain;
using Bloggie.Web.Models.ViewModels;
using Bloggie.Web.Repositories.Intefaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace Bloggie.Web.Pages.Admin.Blogs
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel        
    {
        private readonly BloggieDbContext _bloggieDbContext;
        private readonly IBlogPostRepository _blogpostRepository;
        [BindProperty]
        public EditBlogPostRequest BlogPost { get; set; }
        [BindProperty]
        public IFormFile? FeaturedImage { get; set; }
        [BindProperty]
        [Required]
        public string Tags { get; set; }
        public EditModel(IBlogPostRepository blogPostRepository )
        {
            
            _blogpostRepository = blogPostRepository;
        }
        public async Task OnGet( Guid id)
        {
            var blogPostDomainModel = await _blogpostRepository.GetAsync(id);
            if(blogPostDomainModel!= null && blogPostDomainModel.Tags !=null)
            {
                BlogPost = new EditBlogPostRequest() 
                {
                    Id = blogPostDomainModel.Id,
                    Heading= blogPostDomainModel.Heading,
                    PageTitle= blogPostDomainModel.PageTitle,
                    Content= blogPostDomainModel.Content,
                    ShortDescription = blogPostDomainModel.ShortDescription,
                    FeaturedImageUrl = blogPostDomainModel.FeaturedImageUrl,
                    UrlHandle= blogPostDomainModel.UrlHandle,
                    PublishedDate= blogPostDomainModel.PublishedDate,
                    Author = blogPostDomainModel.Author,
                    Visible = blogPostDomainModel.Visible
                    
                };
                Tags = string.Join(",", blogPostDomainModel.Tags.Select(t => t.Name));
            }
            
                        

        }
        public async Task<IActionResult> OnPostEdit()
        {
            ValidateEditBlogPost();

            if (ModelState.IsValid)
            {
                try
                {
                    var blogPostDomainModel = new BlogPost
                    {
                        Id = BlogPost.Id,
                        Heading = BlogPost.Heading,
                        PageTitle = BlogPost.PageTitle,
                        Content = BlogPost.Content,
                        ShortDescription = BlogPost.ShortDescription,
                        FeaturedImageUrl = BlogPost.FeaturedImageUrl,
                        UrlHandle = BlogPost.UrlHandle,
                        PublishedDate = BlogPost.PublishedDate,
                        Author = BlogPost.Author,
                        Visible = BlogPost.Visible,
                        Tags = new List<Tag>(Tags.Split(',').Select(t => new Tag() { Name = t.Trim() }))
                    };



                    await _blogpostRepository.UpdateAsync(blogPostDomainModel);


                    ViewData["Notification"] = new Notification
                    {
                        Message = "Post alterado com sucesso!",
                        Type = NotificationType.Success
                    };
                }
                catch (Exception ex)
                {
                    ViewData["Notification"] = new Notification
                    {
                        Message = $"Erro alterando o post! -  {ex.Message}",
                        Type = NotificationType.Error
                    };

                }
                return Page();

                //TempData["MessageDescription"] = " Post alterado com sucesso!";



                //return RedirectToPage("/Admin/Blogs/List");

                //ViewData["MessageDescription"] = "O Post foi salvo com sucesso!";
                //return Page();
            }
            return Page();

        }

        
        public async Task<IActionResult> OnPostDelete()
        {
            var deleted = await _blogpostRepository.DeleteAsync(BlogPost.Id);
            if (deleted)
            {
                var notification = new Notification
                {
                    Message = "Post excluído com sucesso!",
                    Type = Enums.NotificationType.Success
                };
                TempData["Notification"] = JsonSerializer.Serialize(notification);


                return RedirectToPage("/Admin/Blogs/List");

            }

            return Page();
        }

        private void ValidateEditBlogPost()
        {
            if (!string.IsNullOrWhiteSpace(BlogPost.Heading))
            {
                //checa tamanho minimo
                if(BlogPost.Heading.Length < 10 || BlogPost.Heading.Length > 200)
                {
                    ModelState.AddModelError("BlogPost.Heading",
                        " o tamanho do cabeçalho deve estar entre 10 e 200 caracteres!");
                }

                //checa tamanho máximo
            }
        } 
    }
}
