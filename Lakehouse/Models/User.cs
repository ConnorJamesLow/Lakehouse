using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lakehouse.Models
{
    public class User
    {

        public int UserId { get; set; } = -1;
        public string Name { get; set; } = "";
        public string Phone { get; set; } = "";
        public string Email { get; set; } = "";
        public Role UserRole { get; set; } = Role.Unconfirmed;

    }
}
