using InventoryApp.DTO;
using InventoryApp.Models;

namespace InventoryApp.Service
{
    public interface ITypeService
    {
        Task<Models.Type?> InsertType(TypeCreateDTO? typeDTO);
        Task<bool> UpdateType(int id, TypeUpdateDTO? typeDTO);
        Task<bool> DeleteType(int id);
        Task<Models.Type?> GetTypeById(int id);
        Task<List<Models.Type?>> GetAllTypes();
    }
}
