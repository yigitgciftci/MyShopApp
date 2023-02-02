using Alisveris.Business.Models;
using Alisveris.Business.Models.Category;
using Alisveris.Data;
using Alisveris.Data.Entities;

namespace Alisveris.Business
{
    public interface ICategoryManager
    {
        Category AddSubCategory(int id, SubCategoryCreateModel model);
    }
    public class CategoryManager : ICategoryManager
    {
        private DatabaseContext _context;

        public CategoryManager(DatabaseContext context)
        {
            _context = context;
        }

        public Category AddSubCategory(int id, SubCategoryCreateModel model)
        {
            Category category = _context.Categories.Find(id);
            Category subCategory = _context.Categories.Where(x => x.Name == model.Name).FirstOrDefault();
            category.Subcategory.Add(subCategory);

            _context.SaveChanges();

            return category;
        }
    }
}
