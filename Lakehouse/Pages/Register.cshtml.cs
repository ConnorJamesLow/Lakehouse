using Lakehouse.Managers;
using Lakehouse.Models;
using Lakehouse.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Lakehouse.Pages
{
    public class RegisterModel : PageModel
    {
        public RegisterModel(IConfiguration configuration)
        {
            _userDb = new UserCrud(configuration);
            _session = new SessionService(HttpContext.Session);
        }

        private readonly UserCrud _userDb;
        private readonly SessionService _session;


        [BindProperty]
        public User SessionUser { get; set; }

        [BindProperty] public string Message { get; set; } = "";

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var dbUser = _userDb.GetByName(SessionUser.Name) ?? new User();
            if (dbUser.UserId > 0)
            {
                Message = "Name is already in use.";
                return Page();
            }

            SessionUser.Password = Hasher.Hash(SessionUser.Password);
            await Task.Run(() =>
            {
                _userDb.Add(SessionUser);
            });
            _session.SetUser(SessionUser);
            return RedirectToPage("/App/Dashboard");
        }
    }
}
