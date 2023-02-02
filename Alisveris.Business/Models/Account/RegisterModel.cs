using Alisveris.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alisveris.Data.Models.Account
{
    public class RegisterModel
    {
        [StringLength(25)]
        public string Name { get; set; }
        [StringLength(25)]
        public string Email { get; set; }
        [StringLength(16)]
        public string Password { get; set; }
        [StringLength(11)]
        public string PhoneNumber { get; set; }
        [StringLength(300)]
        public string Address { get; set; }
    }
}
