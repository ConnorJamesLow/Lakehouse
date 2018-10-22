using Lakehouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lakehouse.Managers
{
    public class UserCrud
    {
        private DatabaseContext _context { get; set; }
        public UserCrud()
        {
            if (_context == null)
            {
                _context = new DatabaseContext();
            }
        }

        public User GetById(int id)
        {
            return _context.User.FirstOrDefault(e => e.UserId == id);
        }

        public User GetByName(string name)
        {
            return _context.User.FirstOrDefault(e => e.Name == name);
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
            User before = GetById(user.UserId);
            if (user != null && before != null)
            {
                user.CreationDate = before.CreationDate;

                _context.Entry(before).CurrentValues.SetValues(user);
                _context.SaveChanges();

            }
        }

        public void Delete(int id)
        {
            User before = GetById(id);
            if (before != null)
            {
                _context.Remove(before);
                _context.SaveChanges();

            }
        }
    }
}
