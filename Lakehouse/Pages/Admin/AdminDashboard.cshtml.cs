using Lakehouse.Managers;
using Lakehouse.Models;
using Lakehouse.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lakehouse.Pages.App
{
    public class AdminDashboardModel : PageModel
    {

        [BindProperty]
        public User SessionUser { get; set; }

        public SessionService _session;
        private readonly IReservationCrud _reservations;
        private readonly IUserCrud _users;
        public List<ReservationWithUser> Reservations { get; set; }


        public AdminDashboardModel(IReservationCrud rCrud, IUserCrud uCrud)
        {
            _users = uCrud;
            _reservations = rCrud;
            _session = new SessionService();
        }



        public IActionResult OnGet()
        {
            SessionUser = _session.GetUser(HttpContext.Session);
            if (SessionUser == null || SessionUser.Name.Trim().Length == 0)
            {
                return RedirectToPage("/Logout");
            }
            //verify user is admin
            if (IsHost())
            {
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