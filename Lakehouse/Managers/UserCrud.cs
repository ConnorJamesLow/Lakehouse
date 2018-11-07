using Lakehouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lakehouse.Managers
{
    public class UserCrud
    {
        private DatabaseContext Context { get; }
        public UserCrud()
        {
            if (Context == null)
            {
                Context = new DatabaseContext();
            }
        }

        public User GetById(int id)
        {
            return Context.User.FirstOrDefault(e => e.UserId == id);
        }

        public User GetByName(string name)
        {
            return Context.User.FirstOrDefault(e => e.Name == name);
        }

        public IEnumerable<User> GetAll()
        {
            return Context.User.ToList<User>();
        }

        public void Add(User user)
        {
            user.CreationDate = DateTime.Now;

            Context.Add(user);
            Context.SaveChanges();
        }

        public void Update(User user)
        {
            User before = GetById(user.UserId);
            if (before != null)
            {
                user.CreationDate = before.CreationDate;

                Context.Entry(before).CurrentValues.SetValues(user);
                Context.SaveChanges();

            }
        }

        public void Delete(int id)
        {
            User before = GetById(id);
            if (before != null)
            {
                Context.Remove(before);
                Context.SaveChanges();

            }
        }
    }
}
