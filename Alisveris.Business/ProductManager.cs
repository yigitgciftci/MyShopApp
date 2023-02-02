using Alisveris.Business.Models.Category;
using Alisveris.Data.Entities;
using Alisveris.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alisveris.Data.Models.Product;

namespace Alisveris.Business
{
    public interface IProductManager
    {
        Product AddProduct(ProductCreateModel model);
        Product EditProduct(int id, ProductEditModel model);
        void DeleteProduct(int id);
        List<Product> List();
        Product GetProductById(int id);
    }
    public class ProductManager : IProductManager
    {
        private DatabaseContext _context;
        public ProductManager(DatabaseContext context)
        {
            _context = context;
        }

        public Product AddProduct(ProductCreateModel model)
        {
            Product product = new Product();

            product.Name = model.Name;
            product.Price = model.Price;
            product.Description = model.Description;
            product.Picture = model.Picture;
            product.BarCode = model.BarCode;
            product.CategoryId = model.CategoryId;

            _context.Products.Add(product);
            _context.SaveChanges();

            return product;
        }

        public Product EditProduct(int id, ProductEditModel model)
        {
            Product product = _context.Products.Where(x => x.Id == id).FirstOrDefault();
            product.Name = model.Name;
            product.Price = model.Price;
            product.Description = model.Description;
            product.Picture = model.Picture;
            product.BarCode = model.BarCode;
            
            _context.SaveChanges();

            return product;
        }

        public void DeleteProduct(int id)
        {
            Product product = _context.Products.Where(x => x.Id == id).FirstOrDefault();
            _context.Products.Remove(product);

            _context.SaveChanges();
        }

        public List<Product> List()
        {
            List<Product> products = _context.Products.ToList();
            return products;
        }

        public Product GetProductById(int id)
        {
            Product product = _context.Products.Find(id);
            return product;
        }
    }
}
