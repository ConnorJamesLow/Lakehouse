using System;
using Lakehouse.Models;
using Microsoft.EntityFrameworkCore;

namespace Lakehouse.Managers
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {

        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //string connString = "Data Source = (localdb)\\ProjectsV13; Initial Catalog = Lakehouse";
                //optionsBuilder.UseSqlServer(connString);
                Console.WriteLine("UH OH");
            }
        }


        public DbSet<User> User { get; set; }
    }
}
