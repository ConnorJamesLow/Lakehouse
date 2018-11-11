using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lakehouse.Managers;
using Lakehouse.Models;
using Lakehouse.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace Lakehouse.Pages.App
{
    public class UserRequestsModel : PageModel
    {


        public List<User> Users { get; set; }

        private DatabaseContext _context { get; set; }

        public string Message { get; set; }

        public SessionService _session;

        public UserRequestsModel(DatabaseContext context)
        {
            this._context = context;
            _session = new SessionService();
        }


        public IActionResult OnGet()
        {
            if (isHost())
            {
                getUsers();
                return Page();
            } else
            {
                return RedirectToPage("/App/Error", new { error = "You don't have permission to view this page" });
            }
        }


        public IActionResult OnPostApprove(int userId)
        {
            if (isHost())
            {
                if (userId > 0)
                {

                    User user = _context.User.FirstOrDefault(u => u.UserId == userId);

                    if (user != null)
                    {
                        user.UserRole = Role.Guest;
                        _context.SaveChanges();
                        Message = "Successfully approved: " + user.Name;
                    }
                    else
                    {
                        Message = "No such user";
                    }
                }
                else
                {
                    Message = "No user selected";
                }
                getUsers();
                return Page();
            }
            else
            {
                return RedirectToPage("/App/Error", new { error = "You don't have permission to view this page" });
            }
        }

        public IActionResult OnPostDeny(int userId)
        {
            if (isHost())
            {
                if (userId > 0)
                {

                    User user = _context.User.FirstOrDefault(u => u.UserId == userId);

                    if (user != null)
                    {
                        user.UserRole = Role.Denied;
                        _context.SaveChanges();
                        Message = "Successfully denied: " + user.Name;
                    }
                    else
                    {
                        Message = "No such user";
                    }
                }
                else
                {
                    Message = "No user selected";
                }

                getUsers();
                return Page();
            }
            else
            {
                return RedirectToPage("/App/Error", new { error = "You don't have permission to view this page" });
            }
        }

        public void getUsers()
        {
            Users = _context.User.Where(u => u.UserRole == Role.Unconfirmed || u.UserRole == Role.Denied).ToList<User>();

        }

        public bool isHost()
        {
            User user = _session.GetUser(HttpContext.Session);

            return user?.UserRole == Role.Host;

        }


    }
}