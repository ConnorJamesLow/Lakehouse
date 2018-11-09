using Lakehouse.Managers;
using Lakehouse.Models;
using Lakehouse.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Lakehouse.Pages
{
    public class LoginModel : PageModel
    {
        public LoginModel(IConfiguration configuration)
        {
            _userDb = new UserCrud(configuration);
            _session = new SessionService(HttpContext.Session);
        }

        private readonly UserCrud _userDb;
        private readonly SessionService _session;

        [BindProperty]
        public User SessionUser { get; set; }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var dbUser = _userDb.GetByName(SessionUser.Name);
            var authenticated = await Task.Run(() => Hasher.Compare(SessionUser.Password, dbUser.Password));
            if (authenticated)
            {
                _session.SetUser(SessionUser);
                return RedirectToPage("/App/Dashboard");
            }
            return Page();
        }
    }
}
