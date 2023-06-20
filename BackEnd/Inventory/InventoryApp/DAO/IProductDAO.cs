using InventoryApp.Models;

namespace InventoryApp.DAO
{
    public interface IProductDAO
    {
        Product? Insert (Product product);
        bool Update (int id,  Product product);
        bool Delete (int id);
        Product? GetById (int id);
        Product? GetByModel(string model);
        List<Product?> GetAll();
    }
}
