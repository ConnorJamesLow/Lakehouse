using Lakehouse.Managers;
using Lakehouse.Models;
using Lakehouse.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Lakehouse.Pages
{
    public class LoginModel : PageModel
    {
        private readonly UserCrud _userDb = new UserCrud();

        [BindProperty]
        public User User { get; set; }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var dbUser = _userDb.GetByName(User.Name);
            var authenticated = await Task.Run(() => Hasher.Compare(User.Password, dbUser.Password));

            return authenticated ? (IActionResult) RedirectToPage("/App/Dashboard") : Page();
        }
    }
}
