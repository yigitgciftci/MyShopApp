using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alisveris.Data.Entities
{
    public class Admin
    {
        [Key]
        public int Id { get; set; }
        [StringLength(25)]
        [Required]
        public string Username { get; set; }
        [StringLength(25)]
        [Required]
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
