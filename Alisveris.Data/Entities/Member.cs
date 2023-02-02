using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alisveris.Data.Entities
{
    public class Member
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Address { get; set; }
        public int WalletId { get; set; }
        public int ShoppingCardId { get; set; }
        public Wallet Wallet { get; set; }
        public ShoppingCard ShoppingCard { get; set; }
        public string Role { get; set; }
        public List<Order>? Orders { get; set; }
    }
}
