using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alisveris.Business.Models.Account
{
    public class LoginAdminModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
