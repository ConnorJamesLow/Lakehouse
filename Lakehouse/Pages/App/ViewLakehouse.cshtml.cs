using Lakehouse.Models;
using Lakehouse.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lakehouse.Pages.App
{
    public class ViewLakehouseModel : PageModel
    {
        [BindProperty]
        public User SessionUser { get; set; }

        public ViewLakehouseModel()
        {
            _session = new SessionService();
        }

        private readonly SessionService _session;
        public IActionResult OnGet()
        {
            SessionUser = _session.GetUser(HttpContext.Session);
            if (SessionUser == null || SessionUser.Name.Trim().Length == 0)
            {
                return RedirectToPage("/Logout");
            }
            return Page();
        }
    }
}