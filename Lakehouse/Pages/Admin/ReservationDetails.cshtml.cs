using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Lakehouse.Managers;
using Lakehouse.Models;
using Lakehouse.Services;

namespace Lakehouse.Pages.Admin
{
    public class ReservationDetailsModel : PageModel
    {
        public List<User> Users { get; set; }

        private readonly DatabaseContext _context;

        private IReservationCrud _reservations;

        private readonly IUserCrud _users;

        public List<ReservationWithUser> Reservations { get; set; }

        private readonly SessionService _session;

        public ReservationDetailsModel(DatabaseContext context)
        {
            _context = context;
            _session = new SessionService();
        }

        public void OnGet()
        {
            getReservations();
        }

        public void getReservations()
        {

        }

        public bool isHost()
        {
            User user = _session.GetUser(HttpContext.Session);
            return user?.UserRole == Role.Host;

        }
    }
}