using InventoryApp.DTO;
using InventoryApp.Models;

namespace InventoryApp.Service
{
    public interface IProductService
    {
        Task <Product?> InsertProduct(ProductCreateDTO product);
        Task <bool> UpdateProduct(int id, ProductUpdateDTO product);

        Task<bool> DeleteProduct(int id);
        Task<Product?> GetProductById(int id);
        Task<Product?> GetProductByName(string name);

        Task<List<Product>> GetAllProducts();
    }
}
