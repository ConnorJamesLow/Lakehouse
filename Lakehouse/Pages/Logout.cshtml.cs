using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lakehouse.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lakehouse.Pages
{
    public class LogoutModel : PageModel
    {
        private readonly SessionService _session;
        public LogoutModel()
        {
            _session = new SessionService();
        }

        public void OnGet()
        {
            _session.Clear(HttpContext.Session);
        }
    }
}