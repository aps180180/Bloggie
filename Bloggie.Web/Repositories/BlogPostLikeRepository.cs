using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Bloggie.Web.Repositories.Intefaces;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Repositories
{
    public class BlogPostLikeRepository : IBlogPostLikeRepository
        
    {
        private readonly BloggieDbContext _bloggieDbContext;
        public BlogPostLikeRepository(BloggieDbContext bloggieDbContext)
        {
            _bloggieDbContext = bloggieDbContext;       
        }

        public async Task AddLikeForBlogPost(Guid blogPostid, Guid UserId)
        {
            var like = new BlogPostLike
            {
               Id = new Guid(),
               BlogPostId = blogPostid,
               UserId = UserId
            };
            await _bloggieDbContext.BlogPostLike.AddAsync(like);
            await _bloggieDbContext.SaveChangesAsync();
        }

        public async  Task<IEnumerable<BlogPostLike>> GetLikesForBlogPost(Guid blogPostid)
        {
            return await _bloggieDbContext.BlogPostLike.Where(x=> x.BlogPostId == blogPostid ).ToListAsync();  
        }

        public async Task<int> GetTotalLikesForPost(Guid bloPostId)
        {
            return await _bloggieDbContext.BlogPostLike.CountAsync(x=> x.BlogPostId == bloPostId);
        }
    }
}
