using Lakehouse.Models;
using Lakehouse.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lakehouse.Pages.App
{
    public class DashboardModel : PageModel
    {
        public User SessionUser;

        private readonly SessionService _session;

        public bool isHost = false;

        public DashboardModel()
        {
            _session = new SessionService();
        }



        public void OnGet()
        {
            SessionUser = _session.GetUser(HttpContext.Session);

            isHost = SessionUser?.UserRole == Role.Host;
        }



    }
}