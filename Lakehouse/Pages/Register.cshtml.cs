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
        public RegisterModel(IUserCrud userCrud)
        {
            _userDb = userCrud;
            _session = new SessionService(HttpContext.Session);
        }

        private readonly SessionService _session;
        private readonly IUserCrud _userDb;


        [BindProperty]
        public User SessionUser { get; set; }

        [BindProperty] public string Message { get; set; } = "";

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var dbUser = _userDb.GetByName(SessionUser.Name) ?? null;
            if (dbUser != null)
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
