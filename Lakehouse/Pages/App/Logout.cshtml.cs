using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lakehouse.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lakehouse.Pages.App
{
    public class LogoutModel : PageModel
    {


        public IActionResult OnGet()
        {

            HttpContext.Session.Clear();

            return RedirectToPage("/Index");

        }
    }
}