namespace Bloggie.Web.Repositories.Intefaces
{
    public interface IImageRepository
    {
        Task<string> UpaloadAsync(IFormFile file);
    }
}
