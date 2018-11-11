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
            SessionUser = _session.GetUser(HttpContext.Session) ?? new User();
            if (SessionUser.UserId < 1)
            {
                RedirectToPage("Login");
            }
        }
    }
}