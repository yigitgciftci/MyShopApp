using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alisveris.Business.Models.Member
{
    public class LoginMemberModel
    {
        [StringLength(25)]
        [Required]
        public string Email { get; set; }
        [StringLength(25)]
        [Required]
        public string Password { get; set; }
    }
}
