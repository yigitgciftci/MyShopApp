using Alisveris.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alisveris.Data.Models.Order
{
    public class OrderCreateModel
    {
        public ShoppingCard ShoppingCard { get; set; }
    }
}
