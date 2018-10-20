using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Lakehouse.Data_layer;
using Lakehouse.Models;

namespace Lakehouse.Pages
{
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
 
            //AddUser();
            //UpdateUser();
            DeleteUser();
            //GetUser();
            //GetUserList();
        }

        private void AddUser()
        {
            User user = new User();
            user.Email = "Hank@Gmail.com";
            user.Name = "Hank Smith";
            user.Phone = "749-999-1230";
            user.UserRole = Role.Unconfirmed;
            user.Password = "weert";
            user.UserId = 4;

            UserCrud uc = new UserCrud();
            uc.Add(user);

        }

        private void UpdateUser()
        {
            User user = new User();
            user.Email = "Hank@Gmail.com";
            user.Name = "Hank hat";
            user.Phone = "749-999-1230";
            user.UserRole = Role.Unconfirmed;
            user.Password = "weert";
            user.UserId = 3;

            UserCrud uc = new UserCrud();
            uc.Update(user);
        }

        private void DeleteUser()
        {
            UserCrud uc = new UserCrud();

            User user = uc.Get(1);
            uc.Delete(user);
            User user2 = uc.Get(1);
        }

        private void GetUser()
        {
            UserCrud uc = new UserCrud();

            User user=uc.Get(1);
            string email = user.Email;


        }

        private void GetUserList()
        {
            UserCrud uc = new UserCrud();
            List<User> users = uc.GetAll().ToList();

            int userCount = users.Count;
            foreach (var user in users)
            {
                string email = user.Email;
            }

        }
    }
}
