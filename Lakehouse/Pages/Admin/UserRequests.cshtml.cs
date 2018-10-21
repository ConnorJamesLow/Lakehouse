using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lakehouse.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lakehouse.Pages.App
{
    public class UserRequestsModel : PageModel
    {

        public List<User> Users { get; set; }

        public void OnGet()
        {


            //verify user is admin


            Users = new List<User>();

            for (int i = 0; i < 10; i++)
            {
                Users.Add(new User { UserId = i, Name = "Test user "+i});
            }



        }
    }
}