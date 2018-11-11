using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lakehouse.Pages.App
{
    public class ErrorModel : PageModel
    {

        public string Error;


        public void OnGet(string error)
        {
            this.Error = error;
        }



    }
}