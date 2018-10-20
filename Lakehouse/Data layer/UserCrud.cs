using Lakehouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lakehouse.Data_layer
{
    public class UserCrud
    {
        private Context _context { get; set; }
        public UserCrud(Context context)
        {
            _context = context;
        }
        public User Get(int ID)
        {
            return _context.User.FirstOrDefault(e => e.Id == ID);
        }
        public IEnumerable<User> GetAll()
        {
            return _context.User.ToList<User>();
        }
        public void Add(User user)
        {
            _context.Add(user);
            _context.SaveChanges();
        }

    }
}
