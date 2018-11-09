using Lakehouse.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Lakehouse.Managers
{
    public class DatabaseContext : DbContext
    {
        private readonly IConfiguration _configuration;


        public DatabaseContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                string connString = _configuration.GetConnectionString("main");
                optionsBuilder.UseSqlServer(connString);

            }
        }


        public DbSet<User> User { get; set; }
    }
}
