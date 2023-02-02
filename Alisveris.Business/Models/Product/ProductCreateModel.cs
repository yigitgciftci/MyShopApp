using Alisveris.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alisveris.Data.Models.Product
{
    public class ProductCreateModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? BarCode { get; set; }
        public double Price { get; set; }
        public string? Picture { get; set; }
        public int CategoryId { get; set; }
    }
}
