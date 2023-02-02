using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alisveris.Business.Models.Admin
{
    public class RegisterAdminModel
    {
        [StringLength(25)]
        [Required]
        public string Username { get; set; }
        [StringLength(25)]
        [Required]
        public string Password { get; set; }
    }
}
