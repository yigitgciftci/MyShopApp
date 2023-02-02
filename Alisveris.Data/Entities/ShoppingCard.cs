using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alisveris.Data.Entities
{
    public class ShoppingCard
    {
        public int Id { get; set; }
        public List<Product>? Products { get; set; }
        [ForeignKey("Member")]
        public int MemberId { get; set; }
        public Member Member { get; set; }
    }
}
