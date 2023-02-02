using Alisveris.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alisveris.Business.Models.Category
{
    public class CategoryCreateModel
    {
        public string Name { get; set; }
    }
}
