using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lakehouse.Models
{
    public class User
    {

        [HiddenInput]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int UserId { get; set; }

        public string Name { get; set; }

        [Phone]
        public string Phone { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string Password { get; set; }

        [HiddenInput]
        public int Role { get; set; } = 0;

        [HiddenInput]
        public DateTime CreationDate { get; set; }


        [NotMapped]
        public Role UserRole
        {
            get { return ConvertToRole(Role); }
            set { Role = ConvertToInt(value); }
        }




        private Role ConvertToRole(int role)
        {
            switch (role)
            {
                case 0:
                    return Lakehouse.Models.Role.Unconfirmed;
                case 1:
                    return Lakehouse.Models.Role.Denied;
                case 2:
                    return Lakehouse.Models.Role.Guest;
                case 3:
                    return Lakehouse.Models.Role.Host;

                default:
                    return Lakehouse.Models.Role.Unconfirmed;
            }
        }

        private int ConvertToInt(Lakehouse.Models.Role role)
        {
            switch (role)
            {
                case Lakehouse.Models.Role.Unconfirmed:
                    return 0;
                case Lakehouse.Models.Role.Denied:
                    return 1;
                case Lakehouse.Models.Role.Guest:
                    return 2;
                case Lakehouse.Models.Role.Host:
                    return 3;
                default:
                    return 0;
            }
        }

    }


}
