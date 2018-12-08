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
    public class ReservationEditModel : PageModel
    {

        private readonly DatabaseContext _context;

        private IReservationCrud _reservations;

        private readonly IUserCrud _users;

        public string Message { get; set; }

        [BindProperty]
        public ReservationWithUser Reservation { get; set; }

        private readonly SessionService _session;



        public ReservationEditModel(IReservationCrud rCrud, IUserCrud uCrud, DatabaseContext context)
        {
            _context = context;
            _users = uCrud;
            _reservations = rCrud;
            _session = new SessionService();
        }

        public IActionResult OnPost()
        {

            if (IsHost() || IsCreator())
            {

                Reservation dbReservation = _context.Reservation.SingleOrDefault(r => r.ReservationId == Reservation.ReservationId);

                if (dbReservation != null)
                {
                    dbReservation.StartDate = Reservation.StartDate;
                    dbReservation.EndDate = Reservation.EndDate;

                    _context.SaveChanges();

                    return RedirectToPage("/Admin/AdminDashboard");
                }

                return RedirectToPage("/App/Error", new { error = "Failed to update reservation" });

            }

            return RedirectToPage("/App/Error", new { error = "You don't have permission to view this page" });
        }

        public IActionResult OnGet(int reservationId)
        {


            Reservation res = _context.Reservation.SingleOrDefault(r => r.ReservationId == reservationId);

            if (res != null)
            {
                Reservation = new ReservationWithUser
                {
                    ReservationId = res.ReservationId,
                    UserId = res.UserId,
                    StartDate = res.StartDate,
                    EndDate = res.EndDate,
                    User = _users.GetById(res.UserId)

                };
            }


            if (IsHost() || IsCreator())
            {

                return Page();
            }

            return RedirectToPage("/App/Error", new { error = "You don't have permission to view this page" });
        }

        private bool IsCreator()
        {
            User user = _session.GetUser(HttpContext.Session);
            return user?.UserId == Reservation.UserId;
        }

        private bool IsHost()
        {
            User user = _session.GetUser(HttpContext.Session);
            return user?.UserRole == Role.Host;

        }
    }
}