using Lakehouse.Models;
using Lakehouse.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lakehouse.Pages.App
{
    public class UserStatusModel : PageModel
    {
        private readonly SessionService _session;

        public UserStatusModel()
        {
            _session = new SessionService();
        }

        public User SessionUser { get; set; }
        public void OnGet()
        {
            SessionUser = _session.GetUser(HttpContext.Session);
            if (SessionUser == null)
            {
                RedirectToPage("Login");
            }
        }
    }
}