using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lakehouse.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lakehouse.Pages.App
{
    public class UserStatusModel : PageModel
    {

        public User User { get; set; }
        public void OnGet()
        {
        }
    }
}