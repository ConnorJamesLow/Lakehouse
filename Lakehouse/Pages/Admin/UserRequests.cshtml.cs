using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lakehouse.Managers;
using Lakehouse.Models;
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

        public UserRequestsModel(DatabaseContext context)
        {
            this._context = context;
        }


        public IActionResult OnGet()
        {
            if (isHost())
            {
                getUsers();
                return Page();
            } else
            {
                return RedirectToPage("/App/Error");
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
                return RedirectToPage("/App/Error");
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
                return RedirectToPage("/App/Error");
            }
        }

        public void getUsers()
        {
            Users = _context.User.Where(u => u.UserRole == Role.Unconfirmed || u.UserRole == Role.Denied).ToList<User>();

        }

        public bool isHost()
        {

            //todo: add host role check here

            return true;
        }


    }
}