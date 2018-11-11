using Lakehouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace Lakehouse.Managers
{

    public interface IUserCrud
    {
 
        User GetById(int id);
        User GetByName(string name);
        IEnumerable<User> GetAll();
        Boolean Add(User user);
        Boolean Update(User user);
        Boolean Delete(int id);
    }



    //to use userCrud, simply create a default UserCrud class object
    //on the UserCrud class object call the method of your choice
    //if the method returns false, 0 or null a crash has happen
    public class UserCrud : IUserCrud
    {
        //this stores the connection to the database
        private DatabaseContext _context { get; set; }

        //populate the _connext when this class is created
        public UserCrud(DatabaseContext dbContext)
        {
            this._context = dbContext;
        }

        //gets the first user that matches with the id in the database
        //retirns null if an error has happened
        public User GetById(int id)
        {
            try
            {
                return _context.User.FirstOrDefault(e => e.UserId == id);

            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (System.Data.SqlClient.SqlException sqlEx)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                throw new Exception("Incorrect connection string. Fix the problem by opening DatabaseContext in Managers folder. Then alter line 23 to connect to the correct database.");
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (Exception e)
#pragma warning restore CS0168 // Variable is declared but never used
            {

                throw new Exception("invalid ID of" + id + " when calling GetByID method in class UserCrud");
            }
        }

        //gets the first user that matches with the name in the database
        //retirns null if an error has happened
        public User GetByName(string name)
        {
            try
            {
                return _context.User.FirstOrDefault(e => e.Name == name);
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (System.Data.SqlClient.SqlException sqlEx)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                throw new Exception("Incorrect connection string. Fix the problem by opening DatabaseContext in Managers folder. Then alter line 23 to connect to the correct database.");
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (Exception e)
#pragma warning restore CS0168 // Variable is declared but never used
            {

                throw new Exception("invalid name of" + name + " when calling GetByName method in class UserCrud");
            }
        }

        //gets all users in the database
        //retirns null if an error has happened
        public IEnumerable<User> GetAll()
        {
            try
            {
                return _context.User.ToList<User>();
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (System.Data.SqlClient.SqlException sqlEx)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                throw new Exception("Incorrect connection string. Fix the problem by opening DatabaseContext in Managers folder. Then alter line 23 to connect to the correct database.");
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (Exception e)
#pragma warning restore CS0168 // Variable is declared but never used
            {

                throw new Exception("GetAll method in class UserCrud has crashed. Make sure there is users in the database");
            }
        }

        //adds user a new user, with the time it was added to the database, into database
        //retirns false if an error has happened
        public Boolean Add(User user)
        {
            try
            {
                user.CreationDate = DateTime.Now;

                _context.Add(user);
                _context.SaveChanges();

                return true;
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (System.Data.SqlClient.SqlException sqlEx)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                throw new Exception("Incorrect connection string. Fix the problem by opening DatabaseContext in Managers folder. Then alter line 23 to connect to the correct database.");
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (Exception e)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                String message = "invalid SessionUser that has";
                if (user.Name == null)
                {
                    message += "Name = null, ";
                }
                else
                {
                    message += "Name = " + user.Name + ", ";
                }
                if (user.Password == null)
                {
                    message += "Password = null, ";
                }
                else
                {
                    message += "Password = " + user.Password + ", ";
                }
                if (user.Phone == null)
                {
                    message += "Phone = null, ";
                }
                else
                {
                    message += "Phone = " + user.Phone + ", ";
                }

                message += "Role number = " + user.Role + " which means Role = " + user.UserRole.ToString() + ", ";

                if (user.Email == null)
                {
                    message += "Email = null, and ";
                }
                else
                {
                    message += "Email = " + user.Email + ", and ";
                }
                if (user.CreationDate == null)
                {
                    message += "Creation date = null. ";
                }
                else
                {
                    message += "Creation date = " + user.CreationDate + ". ";
                }
                message += "when calling Add method in class UserCrud";
                throw new Exception(message);
            }
        }

        //edit the user in the database by editing the user in the database that has the same id
        //returns false if error has happened
        public Boolean Update(User user)
        {
            try
            {
                User before = GetById(user.UserId);
                if (user != null && before != null)
                {
                    user.CreationDate = before.CreationDate;

                    _context.Entry(before).CurrentValues.SetValues(user);
                    _context.SaveChanges();
                    return true;
                }
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (System.Data.SqlClient.SqlException sqlEx)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                throw new Exception("Incorrect connection string. Fix the problem by opening DatabaseContext in Managers folder. Then alter line 23 to connect to the correct database.");
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (Exception e)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                String message = "invalid SessionUser that has";
                if (user.Name == null)
                {
                    message += "Name = null, ";
                }
                else
                {
                    message += "Name = " + user.Name + ", ";
                }
                if (user.Password == null)
                {
                    message += "Password = null, ";
                }
                else
                {
                    message += "Password = " + user.Password + ", ";
                }
                if (user.Phone == null)
                {
                    message += "Phone = null, ";
                }
                else
                {
                    message += "Phone = " + user.Phone + ", ";
                }

                message += "Role number = " + user.Role + " which means Role = " + user.UserRole.ToString() + ", ";

                if (user.Email == null)
                {
                    message += "Email = null, and ";
                }
                else
                {
                    message += "Email = " + user.Email + ", and ";
                }
                if (user.CreationDate == null)
                {
                    message += "Creation date = null. ";
                }
                else
                {
                    message += "Creation date = " + user.CreationDate + ". ";
                }
                message += "when calling Add method in class UserCrud";
                throw new Exception(message);
            }
            return false;
        }

        //delete the first user that matches the id
        //returns false if the user does not exist of an error has happened
        public Boolean Delete(int id)
        {
            try
            {
                User before = GetById(id);
                if (before != null)
                {
                    _context.Remove(before);
                    _context.SaveChanges();

                    return true;
                }
            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                Console.WriteLine(sqlEx + "\n");
                throw new Exception("Incorrect connection string. Fix the problem by opening DatabaseContext in Managers folder. Then alter line 23 to connect to the correct database.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e + "\n");
                throw new Exception("invalid ID of" + id + " when calling Delete method in class UserCrud");
            }
            return false;
        }
    }
}
