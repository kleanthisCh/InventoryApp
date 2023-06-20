using InventoryApp.Models;

namespace InventoryApp.DAO
{
    public interface IManufacturerDAO
    {
        Manufacturer? Insert (Manufacturer manufacturer);
        bool Update (int id, Manufacturer manufacturer);
        bool Delete (int id);
        Manufacturer? GetById (int id);
        List<Manufacturer?> GetAll ();
    }
}
