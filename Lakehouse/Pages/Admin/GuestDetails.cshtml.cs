using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lakehouse.Managers;
using Lakehouse.Models;
using Lakehouse.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lakehouse.Pages.Admin
{
    public class GuestDetailsModel : PageModel
    {

        private readonly DatabaseContext _context;

        public string Message { get; set; }

        private readonly SessionService _session;

        public GuestDetailsModel(DatabaseContext context)
        {
            _context = context;
            _session = new SessionService();
        }


        public User User { get; set; }


        public IActionResult OnGet(int userId)
        {
            if (isHost())
            {

                User = _context.User.SingleOrDefault(u => u.UserId == userId);

                return Page();
            }
            return RedirectToPage("/App/Error", new { error = "You don't have permission to view this page" });
        }


        public bool isHost()
        {
            User user = _session.GetUser(HttpContext.Session);
            return user?.UserRole == Role.Host;

        }
    }
}