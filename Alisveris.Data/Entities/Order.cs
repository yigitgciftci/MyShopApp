using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alisveris.Data.Entities
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        [StringLength(6)]
        public string OrderNumber { get; set; }
        public ShoppingCard ShoppingCard { get; set; }
        public DateTime OrderDate { get; set; }
        public int MemberId { get; set; }
        public Member Member { get; set; }
    }
}
