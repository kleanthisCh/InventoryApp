using InventoryApp.Models;
using InventoryApp.DTO;

namespace InventoryApp.Service
{
    public interface IManufacturerService
    {
        Task <Manufacturer?> InsertManufacturer(ManufacturerCreateDTO? manufacturerDTO);
        Task <bool> UpdateManufacturer (int id, ManufacturerUpdateDTO? manufacturerDTO);
        Task <bool> DeleteManufacturer (int id);
        Task<Manufacturer?> GetManufacturerById (int id);
        Task<List<Manufacturer?>> GetAllManufacturers();

    }
}
