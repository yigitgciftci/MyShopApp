using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alisveris.Data.Models.Account
{
    public class EditPasswordModel
    {
        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(16)]
        public string Password { get; set; }
    }
}
