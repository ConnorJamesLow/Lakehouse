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
    public class DashboardModel : PageModel
    {
        public User UserSession;

        private readonly SessionService _session;

        public bool isHost = false;

        public DashboardModel()
        {
            _session = new SessionService();
        }



        public void OnGet()
        {
            UserSession = _session.GetUser(HttpContext.Session);

            isHost = UserSession?.UserRole == Role.Host;
        }



    }
}