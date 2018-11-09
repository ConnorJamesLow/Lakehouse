using Lakehouse.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Lakehouse.Services
{
    public class SessionService
    {
        public static ISession Session { get; set; }

        public SessionService(ISession session)
        {
            Session = session;
        }

        public void SetUser(User user)
        {
            Session.SetString("user", JsonConvert.SerializeObject(user));
        }

        public User GetUser()
        {
            string json = Session.GetString("user");
            User user = JsonConvert.DeserializeObject<User>(json);
            return user;
        }
    }
}
