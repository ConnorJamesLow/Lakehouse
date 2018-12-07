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

        public string Message { get; set; }

        public List<ReservationWithUser> Reservations { get; set; }

        private readonly SessionService _session;

        public ReservationDetailsModel(IReservationCrud rCrud, IUserCrud uCrud)
        {
            _users = uCrud;
            _reservations = rCrud;
            _session = new SessionService();
        }

        public IActionResult OnGet()
        {
            if (IsHost())
            {

                Reservations = _reservations.GetAll().Select(reservation => new ReservationWithUser
                {
                    ReservationId = reservation.ReservationId,
                    UserId = reservation.UserId,
                    StartDate = reservation.StartDate,
                    EndDate = reservation.EndDate,
                    User = _users.GetById(reservation.UserId)

                }).ToList();


                return Page();
            }

            return RedirectToPage("/App/Error", new { error = "You don't have permission to view this page" });
        }

        

        public bool IsHost()
        {
            User user = _session.GetUser(HttpContext.Session);
            return user?.UserRole == Role.Host;

        }
    }
}