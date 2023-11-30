using Bloggie.Web.Models.Domain;

namespace Bloggie.Web.Repositories.Intefaces
{
    public interface IBlogPostLikeRepository
    {
        Task<int> GetTotalLikesForPost(Guid bloPostId);
        Task AddLikeForBlogPost(Guid blogPostid, Guid UserId);
        Task<IEnumerable<BlogPostLike>> GetLikesForBlogPost(Guid blogPostid);
    }
}
