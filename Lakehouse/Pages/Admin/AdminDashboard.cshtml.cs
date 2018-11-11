using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lakehouse.Models;
using Lakehouse.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lakehouse.Pages.App
{
    public class AdminDashboardModel : PageModel
    {

        public SessionService _session;

        public AdminDashboardModel()
        {

            _session = new SessionService();
        }



        public IActionResult OnGet()
        {

            //verify user is admin
            if (isHost())
            {

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