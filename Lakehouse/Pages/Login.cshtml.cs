using Lakehouse.Managers;
using Lakehouse.Models.ViewModels;
using Lakehouse.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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

        [BindProperty]
        public Login SessionUser { get; set; }

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
                _session.SetUser(dbUser, HttpContext.Session);
                return RedirectToPage("/App/Dashboard");
            }
            return Page();
        }
    }
}
