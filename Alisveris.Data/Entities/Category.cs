using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alisveris.Data.Entities
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Product>? Products { get; set; }

        [ForeignKey("SubcategoryId")]
        public int SubcategoryId { get; set; }
        public List<Category> Subcategory { get; set; } = new List<Category>();

    }
}
