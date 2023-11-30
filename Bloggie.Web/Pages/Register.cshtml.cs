using Bloggie.Web.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bloggie.Web.Pages
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public Register RegisterViewModel { get; set; }
        private readonly UserManager<IdentityUser> _userManager;
        public RegisterModel(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)

            {
                var user = new IdentityUser
                {
                    UserName = RegisterViewModel.Username,
                    Email = RegisterViewModel.Email,
                };

                var identityResult = await _userManager.CreateAsync(user, RegisterViewModel.Password);

                if (identityResult.Succeeded)
                {
                    var addRolesResult = await _userManager.AddToRoleAsync(user, "user");
                    if (addRolesResult.Succeeded)
                    {
                        ViewData["Notification"] = new Notification
                        {
                            Type = Enums.NotificationType.Success,
                            Message = "Conta criada com sucesso!"
                        };
                        return Page();
                    }

                }

                ViewData["Notification"] = new Notification
                {
                    Type = Enums.NotificationType.Error,
                    Message = "Ocorreu um erro ao criar a conta!"
                };
                return Page();

            }
            else
            {
                return Page();
            }

        }
    }
}
