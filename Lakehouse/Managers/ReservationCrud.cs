using Lakehouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lakehouse.Managers
{
    public interface IReservationCrud
    {

        Reservation GetById(int id);
        IEnumerable<Reservation> GetAll();
        Boolean Add(Reservation Reservation);
        Boolean Update(Reservation Reservation);
        Boolean Delete(int id);
    }

    public class ReservationCrud : IReservationCrud
    {
        //this stores the connection to the database
        private DatabaseContext _context { get; set; }

        //populate the _connext when this class is created
        public ReservationCrud(DatabaseContext dbContext)
        {
            this._context = dbContext;
        }

        //gets the first Reservation that matches with the id in the database
        //retirns null if an error has happened
        public Reservation GetById(int id)
        {
            try
            {
                return _context.Reservation.FirstOrDefault(e => e.ReservationId == id);

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

                throw new Exception("invalid ID of" + id + " when calling GetByID method in class ReservationCrud");
            }
        }

        //gets all Reservations in the database
        //retirns null if an error has happened
        public IEnumerable<Reservation> GetAll()
        {
            try
            {
                return _context.Reservation.ToList<Reservation>();
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

                throw new Exception("GetAll method in class ReservationCrud has crashed. Make sure there is Reservations in the database");
            }
        }

        //adds Reservation a new Reservation into database
        //retirns false if an error has happened
        public Boolean Add(Reservation Reservation)
        {
            try
            {
                _context.Add(Reservation);
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
                String message = "invalid Reservation that has";
                if (Reservation.UserId <=0)
                {
                    message += "UserId = null, ";
                }
                else
                {
                    message += "UserId = " + Reservation.UserId + ", ";
                }
                if (Reservation.StartDate == null)
                {
                    message += "StartDate = null, ";
                }
                else
                {
                    message += "StartDate = " + Reservation.StartDate;
                }
                if (Reservation.EndDate == null)
                {
                    message += "EndDate = null, ";
                }
                else
                {
                    message += "EndDate = " + Reservation.EndDate;
                }

                
                message += "when calling Add method in class ReservationCrud";
                throw new Exception(message);
            }
        }

        //edit the Reservation in the database by editing the Reservation in the database that has the same id
        //returns false if error has happened
        public Boolean Update(Reservation Reservation)
        {
            try
            {
                Reservation before = GetById(Reservation.ReservationId);
                if (Reservation != null && before != null)
                {
                    _context.Entry(before).CurrentValues.SetValues(Reservation);
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
                String message = "invalid Reservation that has";
                if (Reservation.UserId <= 0)
                {
                    message += "UserId = null, ";
                }
                else
                {
                    message += "UserId = " + Reservation.UserId + ", ";
                }
                if (Reservation.StartDate == null)
                {
                    message += "StartDate = null, ";
                }
                else
                {
                    message += "StartDate = " + Reservation.StartDate + ", ";
                }
                if (Reservation.EndDate == null)
                {
                    message += "EndDate = null, ";
                }
                else
                {
                    message += "EndDate = " + Reservation.EndDate + ", ";
                }

                message += "when calling Add method in class ReservationCrud";
                throw new Exception(message);
            }
            return false;
        }

        //delete the first Reservation that matches the id
        //returns false if the Reservation does not exist of an error has happened
        public Boolean Delete(int id)
        {
            try
            {
                Reservation before = GetById(id);
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
                throw new Exception("invalid ID of" + id + " when calling Delete method in class ReservationCrud");
            }
            return false;
        }
    }
}
