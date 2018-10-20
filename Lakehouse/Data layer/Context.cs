using Lakehouse.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lakehouse.Data_layer
{
    public class LakeHouseContext : DbContext
    {
        public LakeHouseContext() : base()
        {

        }

        public LakeHouseContext(DbContextOptions<LakeHouseContext> options) : base(options)
        {
        }

        protected override  void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connString = "Data Source = (localdb)\\ProjectsV13; Initial Catalog = Lakehouse";
                optionsBuilder.UseSqlServer(connString);
            }
        }


        public DbSet<User> User { get; set; }
    }
}
