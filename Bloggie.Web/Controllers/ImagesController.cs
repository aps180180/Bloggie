using Bloggie.Web.Repositories.Intefaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Bloggie.Web.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ImagesController : Controller
    {
        private readonly IImageRepository _imageRepository;
        public ImagesController( IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;         
        }
        [HttpPost]
        public async Task<IActionResult> UploadAsync(IFormFile file)
        {
           var imageUrl= await _imageRepository.UpaloadAsync(file);
           if(imageUrl == null)
            {
                return Problem("Ocorreu um erro ao subir a imagem",null,(int )HttpStatusCode.InternalServerError);  
            } 
           return Json(new { Link =imageUrl });
        }

        
    }
}
