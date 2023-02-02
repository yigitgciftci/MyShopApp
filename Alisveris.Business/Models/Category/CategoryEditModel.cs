using Alisveris.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alisveris.Business.Models.Category
{
    public class CategoryEditModel
    {
        public string Name { get; set; }
        public int SubcategoryId { get; set; }
    }
}
