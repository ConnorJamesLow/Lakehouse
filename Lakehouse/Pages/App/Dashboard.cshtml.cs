using Lakehouse.Managers;
using Lakehouse.Models;
using Lakehouse.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Lakehouse.Pages.App
{
    public class DashboardModel : PageModel
    {
        [BindProperty]
        public User SessionUser { get; set; }

        private readonly IReservationCrud _reservations;
        private readonly IUserCrud _users;
        private readonly SessionService _session;
        public List<ReservationWithUser> Reservations { get; set; }

        public bool IsHost;

        public DashboardModel(IReservationCrud rCrud, IUserCrud uCrud)
        {
            _users = uCrud;
            _reservations = rCrud;
            _session = new SessionService();
            try
            {

                Reservations = _reservations.GetAll().Select(reservation => new ReservationWithUser
                {
                    ReservationId = reservation.ReservationId,
                    UserId = reservation.UserId,
                    StartDate = reservation.StartDate,
                    EndDate = reservation.EndDate,
                    User = _users.GetById(reservation.UserId)

                }).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }



        public IActionResult OnGet()
        {
            SessionUser = _session.GetUser(HttpContext.Session);
            if (SessionUser == null || SessionUser.Name.Trim().Length == 0)
            {
                return RedirectToPage("/Logout");
            }
            IsHost = SessionUser?.UserRole == Role.Host;
            return Page();
        }



    }
}