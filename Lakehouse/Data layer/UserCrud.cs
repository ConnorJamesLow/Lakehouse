using Lakehouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lakehouse.Data_layer
{
    public class UserCrud
    {
        private LakeHouseContext _context { get; set; }
        public UserCrud()
        {
            if (_context == null)
            {
                _context = new LakeHouseContext();
            }
        }
        public User Get(int ID)
        {
            return _context.User.FirstOrDefault(e => e.UserId == ID);
        }
        public IEnumerable<User> GetAll()
        {
            return _context.User.ToList<User>();
        }
        public void Add(User user)
        {
            user.CreationDate = DateTime.Now;

            _context.Add(user);
            _context.SaveChanges();
        }
        public void Update(User user)
        {
            User before = Get(user.UserId);
            if(user != null && before != null)
            {
                user.CreationDate = before.CreationDate;

                _context.Entry(before).CurrentValues.SetValues(user);
                _context.SaveChanges();

            }
        }
        public void Delete(int id)
        {
            User before = Get(id);
            if (before != null)
            {
                _context.Remove(before);
                _context.SaveChanges();

            }
        }
    }
}
