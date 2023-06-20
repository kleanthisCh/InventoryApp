using InventoryApp.Data;
using InventoryApp.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryApp.DAO
{
    public class ProductDAOImpl : IProductDAO
    {
        public readonly InventoryContext _context;
        
        public ProductDAOImpl(InventoryContext context)
        {
            this._context = context;
        }

        public Product? Insert(Product product)
        {
            if (_context.Products ==  null)
            {
                return null;
            }
            _context.Products.Add(product);
            return product;
        }

        public bool Update(int id, Product product)
        {
            if (id != product.ProductId)
            {
                return false;
            }
            var testProduct = GetById(id);
            if (testProduct == null)
            {
                return false;
            }

            _context.Products.Update(product);
            return true;
        }

        public bool Delete(int id)
        {
            if (_context.Products == null)
            {
                return false;
            }
            var product = GetById(id);
            if (product == null)
            {
                return false;
            }

            _context.Products.Remove(product);
            return true;
        }

        public Product? GetById(int id)
        {
            if (_context.Products == null)
            {
                return null;
            }

            Product? product = _context.Products.AsNoTracking().Where(x => x.ProductId == id).FirstOrDefault();
            return product;
        }

        public Product? GetByModel(string model)
        {
            if (_context.Products == null)
            {
                return null;
            }

            Product? product = _context.Products.AsNoTracking().Where(x => x.Model == model).FirstOrDefault();
            return product;
        }


        public List<Product?> GetAll()
        {
            var list = new List<Product?>();
            if (_context.Products == null)
            {
                return list;
            }
            var products = _context.Products.ToList();
            return products;
        }

        
        
    }
}
