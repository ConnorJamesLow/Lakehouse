using Lakehouse.Managers;
using Lakehouse.Models;
using Lakehouse.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Lakehouse.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly UserCrud _userDb = new UserCrud();


        [BindProperty]
        public User User { get; set; }

        [BindProperty] public string Message { get; set; } = "";

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var dbUser = _userDb.GetByName(User.Name) ?? new User();
            if (dbUser.UserId > 0)
            {
                Message = "Name is already in use.";
                return Page();
            }

            User.Password = Hasher.Hash(User.Password);
            await Task.Run(() =>
            {
                _userDb.Add(User);
            });
            return RedirectToPage("/App/Dashboard");
        }
    }
}
