using Lakehouse.Managers;
using Lakehouse.Models;
using Lakehouse.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Lakehouse.Pages
{
    public class LoginModel : PageModel
    {
        public LoginModel(IUserCrud userCrud)
        {
            _userDb = userCrud;
            _session = new SessionService();
        }

        private readonly SessionService _session;
        private readonly IUserCrud _userDb;

        [Required]
        [BindProperty]
        public string Name { get; set; }

        [Required]
        [BindProperty]
        public string Password { get; set; }

        [BindProperty]
        public string Message { get; set; }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var dbUser = _userDb.GetByName(Name);
            if (dbUser == null)
            {
                Message = "Credentials did not match any users.";
                return Page();
            }
            ConsoleLogger.Out($"Password: {Password} == {dbUser.Password}");
            var authenticated = await Task.Run(
                () => Hasher.Compare(Password, dbUser.Password)
                );
            if (true)
            {
                _session.SetUser(dbUser, HttpContext.Session);
                switch (dbUser.UserRole)
                {
                    case Role.Host: return RedirectToPage("/Admin/AdminDashboard");
                    case Role.Guest: return RedirectToPage("/App/Dashboard");
                    default: return RedirectToPage("/App/UserStatus");
                }
            }
            Message = "Credentials did not match any users.";
            return Page();
        }
    }
}
