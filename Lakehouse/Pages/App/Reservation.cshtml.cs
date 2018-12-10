using Lakehouse.Managers;
using Lakehouse.Models;
using Lakehouse.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using Newtonsoft.Json;

namespace Lakehouse.Pages.App
{
    public class ReservationModel : PageModel
    {
        [DisplayName("Reservation Start Date")]
        [DataType(DataType.Date)]
        [BindProperty]
        public DateTime StartDate { get; set; }

        [DisplayName("Reservation End Date")]
        [DataType(DataType.Date)]
        [BindProperty]
        public DateTime EndDate { get; set; }

        [BindProperty]
        public string Message { get; set; }

        [BindProperty]
        public User SessionUser { get; set; }

        /// <summary>
        /// The id if the reservation is being updated
        /// </summary>
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        private readonly SessionService _session;
        private readonly IUserCrud _users;
        private readonly IReservationCrud _reservations;
        public List<ReservationWithUser> Reservations { get; set; }


        public ReservationModel(IReservationCrud rCrud, IUserCrud uCrud)
        {

            _session = new SessionService();
            _users = uCrud;
            _reservations = rCrud;
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
            try
            {
                // check credentials in session
                SessionUser = _session.GetUser(HttpContext.Session);
                ConsoleLogger.Out(JsonConvert.SerializeObject(SessionUser));
                if (SessionUser.UserRole != Role.Host && SessionUser.UserRole != Role.Guest)
                {
                    // if invalid, redirect to login
                    return RedirectToPage("/App/Error");
                }

                // check for id parameter
                ConsoleLogger.Out($"{Id}");
                try
                {
                    var reservation = _reservations.GetById(Id);
                    if (reservation?.UserId > 0)
                    {
                        StartDate = DateTime.Today;
                        EndDate = DateTime.Today;
                    }
                    else
                    {
                        StartDate = DateTime.Today;
                        EndDate = DateTime.Today;
                    }

                }
                catch (SqlException e)
                {
                    ConsoleLogger.Out($"Invalid id: {e}");
                    StartDate = DateTime.Today;
                    EndDate = DateTime.Today;
                }
                return Page();
            }
            catch (Exception e)
            {
                ConsoleLogger.Out($"Error: {e}");
                throw;
            }

        }

        public IActionResult OnPost()
        {
            ConsoleLogger.Out($"Start: {StartDate}, End: {EndDate}");
            if (!ModelState.IsValid)
            {
                Message = "Invalid data";
                return Page();
            }
            SessionUser = _session.GetUser(HttpContext.Session);
            var reservation = new Reservation
            {
                UserId = SessionUser.UserId,
                StartDate = StartDate,
                EndDate = EndDate
            };
            var checking = _reservations.check(reservation);
            if (checking)
            {
                Message = "Days conflicted";
                return Page();
            }
            _reservations.Add(reservation);
            return SessionUser.UserRole == Role.Host ? RedirectToPage("/Admin/AdminDashboard") : RedirectToPage("/App/Dashboard");
        }
    }
}