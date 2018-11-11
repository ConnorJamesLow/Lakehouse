using Lakehouse.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Lakehouse.Services
{
    public class SessionService
    {

        public SessionService()
        {
        }

        public void SetUser(User user, ISession session)
        {
            session.SetString("user", JsonConvert.SerializeObject(user));
        }

        public User GetUser(ISession session)
        {
            string json = session.GetString("user");
            User user = JsonConvert.DeserializeObject<User>(json);
            return user;
        }
    }
}
