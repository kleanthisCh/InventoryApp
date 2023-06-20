using InventoryApp.Models;

namespace InventoryApp.DAO
{
    public interface IGenderDAO
    {
        Gender? Insert(Gender gender);
        bool Update(int id, Gender gender);
        bool Delete(int id);
        Gender? GetById(int id);
        List<Gender?> GetAll();
    }
}
