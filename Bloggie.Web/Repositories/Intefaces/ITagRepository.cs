using Bloggie.Web.Models.Domain;

namespace Bloggie.Web.Repositories.Intefaces
{
    public interface ITagRepository
    {   
        Task<IEnumerable<Tag>> GetAllAsync();
    }
}
