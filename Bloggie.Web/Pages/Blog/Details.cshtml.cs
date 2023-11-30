using Bloggie.Web.Models.Domain;
using Bloggie.Web.Models.ViewModels;
using Bloggie.Web.Repositories.Intefaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Bloggie.Web.Pages.Blog
{
   
    public class DetailsModel : PageModel
    {
        private readonly IBlogPostRepository _blogPostRepository;
        private readonly IBlogPostLikeRepository _blogPostLikeRepository;
        private readonly IBlogPostCommentRepository _blogPostCommentRepository;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        public BlogPost BlogPost { get; set; }
        public List<BlogComment> Comments { get; set; }
        public int TotalLikes { get; set; }
        public bool Liked { get; set; }
        [BindProperty]
        public Guid BlogPostId { get; set; }

        [BindProperty]
        [Required]
        [MinLength(1)]
        [MaxLength(300)]
        public string CommentDescription { get; set; }


        public DetailsModel(IBlogPostRepository blogPostRepository,
            IBlogPostLikeRepository blogPostLikeRepository,
            IBlogPostCommentRepository blogPostCommentRepository,
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager
            )
        {
            _blogPostRepository = blogPostRepository;
            _blogPostLikeRepository = blogPostLikeRepository;
            _blogPostCommentRepository = blogPostCommentRepository;
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public async Task<IActionResult> OnGet( string urlHandle)
        {
            await GetPost(urlHandle);
            return Page();
        }

        public async Task<IActionResult> OnPost(string urlHandle) 
        {
            if (ModelState.IsValid)
            {
                if (_signInManager.IsSignedIn(User) && !string.IsNullOrWhiteSpace(CommentDescription))
                {
                    var userId = _userManager.GetUserId(User);
                    var comment = new BlogPostComment()
                    {
                        BlogPostId = BlogPostId,
                        Description = CommentDescription,
                        DateAdded = DateTime.Now,
                        UserId = Guid.Parse(userId)
                    };
                    await _blogPostCommentRepository.AddAsync(comment);
                }
                return RedirectToPage("/Blog/Details", new { urlHandle = urlHandle });
            }
            
            await GetPost(urlHandle);
            return Page();
            
            

        }

        private async Task GetComments()
        {
            var blogPostComents = await _blogPostCommentRepository.GetAllAsync(BlogPost.Id);
            
            var blogCommentsViewModel = new List<BlogComment>();

            foreach (var comment in blogPostComents) 
            {
                blogCommentsViewModel.Add(new BlogComment()
                {
                    Description = comment.Description,
                    DateAdded = comment.DateAdded,
                    UserName = (await _userManager.FindByIdAsync(comment.UserId.ToString())).UserName 
                });
            }

            Comments = blogCommentsViewModel;   
        }

        private  async Task GetPost( string urlHandle)
        {
            BlogPost = await _blogPostRepository.GetAsync(urlHandle);
            if (BlogPost != null)
            {
                BlogPostId = BlogPost.Id;

                if (_signInManager.IsSignedIn(User))
                {
                    var likes = await _blogPostLikeRepository.GetLikesForBlogPost(BlogPost.Id);

                    var userId = _userManager.GetUserId(User);

                    Liked = likes.Any(x => x.UserId == Guid.Parse(userId));
                    await GetComments();


                }

                TotalLikes = await _blogPostLikeRepository.GetTotalLikesForPost(BlogPost.Id);
            }
        }
    }
}
