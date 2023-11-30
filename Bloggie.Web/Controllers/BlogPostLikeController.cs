using Bloggie.Web.Models.ViewModels;
using Bloggie.Web.Repositories.Intefaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace Bloggie.Web.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BlogPostLikeController : Controller
    {
        private readonly IBlogPostLikeRepository _blogPostLikeRepository;
        public BlogPostLikeController( IBlogPostLikeRepository blogPostLikeRepository)
        {
                _blogPostLikeRepository = blogPostLikeRepository;
        }
       
        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddLike([FromBody] AddBlogPostLikeRequest addBlogPostLikeRequest)
        {
            await _blogPostLikeRepository.AddLikeForBlogPost(addBlogPostLikeRequest.BlogPostId, addBlogPostLikeRequest.UserId);
            return Ok();
        }
        
        [HttpGet]
        [Route("{blogPostId:Guid}/totalLikes")]
        public async Task<IActionResult> GetTotalLikes([FromRoute] Guid blogPostId)
        {
            var totalLikes = await _blogPostLikeRepository.GetTotalLikesForPost(blogPostId);
            return Ok(totalLikes);
        }

    }
}
