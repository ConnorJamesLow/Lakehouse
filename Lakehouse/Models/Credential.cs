using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lakehouse.Models
{
    public class Credential
    {
        public int CredentialId { get; set; }
        public int UserId { get; set; }
        public string Password { get; set; }
    }
}
