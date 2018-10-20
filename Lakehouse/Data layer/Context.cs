using Lakehouse.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lakehouse.Data_layer
{
    public class Context : DbContext
    {
        public DbSet<User> User { get; set; }
    }
}
