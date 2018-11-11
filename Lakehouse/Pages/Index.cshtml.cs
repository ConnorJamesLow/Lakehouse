using Lakehouse.Managers;
using Lakehouse.Models;
using Lakehouse.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lakehouse.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IUserCrud _userDb;

        public IndexModel(IUserCrud userCrud)
        {
            _userDb = userCrud;
        }

        public void OnGet()
        {
            // check for existing host
            if (_userDb.GetByName("host") == null)
            {
                User admin = new User
                {
                    Name = "host",
                    Password = Hasher.Hash("lakehouse"),
                    Phone = "(123)456-7890",
                    Email = "e@mail.com",
                    UserRole = Role.Host
                };
                _userDb.Add(admin);
            }
        }
    }
}
