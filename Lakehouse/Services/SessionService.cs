using System;
using Lakehouse.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Lakehouse.Services
{
    public class SessionService
    {

        public void SetUser(User user, ISession session)
        {
            session.SetString("user", JsonConvert.SerializeObject(user));
        }

        public User GetUser(ISession session)
        {
            string json = session.GetString("user") ?? JsonConvert.SerializeObject(new User());
            try
            {

                User user = JsonConvert.DeserializeObject<User>(json);
                return user;
            }
            catch (JsonException e)
            {
                Console.WriteLine(e);
                return new User();
            }
        }

        public void Clear(ISession session)
        {
            session.SetString("user", "");
            session.Clear();
        }
    }
}
