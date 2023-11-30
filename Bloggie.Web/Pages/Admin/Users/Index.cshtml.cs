using Bloggie.Web.Models.ViewModels;
using Bloggie.Web.Repositories;
using Bloggie.Web.Repositories.Intefaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bloggie.Web.Pages.Admin.Users
{
    [Authorize(Roles ="Admin")]
    public class IndexModel : PageModel
    {
        private readonly IUserRepository _userRepository;

        [BindProperty]
        public AddUser AddUserRequest { get; set; }
        public List<User> Users { get; set; }

        [BindProperty]
        public Guid SelectedUserId { get; set; }
        public IndexModel(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<IActionResult> OnGet()
        {
            await GetUsers();
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var identityUser = new IdentityUser()
                {
                    UserName = AddUserRequest.Username,
                    Email = AddUserRequest.Email,

                };
                var roles = new List<string>() { "User" };
                if (AddUserRequest.AdminCheckbox)
                {
                    roles.Add("Admin");
                }
                var result = await _userRepository.Add(identityUser, AddUserRequest.Password, roles);
                if (result)
                {
                    return RedirectToPage("/Admin/Users/Index");
                }
                return Page();
            }
            await GetUsers();
            return Page(); 
            
        }

        public async Task <IActionResult> OnPostDelete()
        {
            await _userRepository.Delete(SelectedUserId);
            return RedirectToPage("/Admin/Users/Index");

        }

        private async Task GetUsers()
        {
            var users = await _userRepository.GetAll();

            Users = new List<User>();
            foreach (var user in users)
            {
                Users.Add(new User()
                {
                    Id = Guid.Parse(user.Id.ToString()),
                    Email = user.Email,
                    UserName = user.UserName
                });
            }
        }
    }
}