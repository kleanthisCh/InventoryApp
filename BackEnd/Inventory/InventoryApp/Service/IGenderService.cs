using InventoryApp.DTO;
using InventoryApp.Models;

namespace InventoryApp.Service
{
    public interface IGenderService
    {
        Task<Gender?> InsertGender(GenderCreateDTO? genderDTO);
        Task<bool> UpdateGender(int id, GenderUpdateDTO? genderDTO);
        Task<bool> DeleteGender(int id);
        Task<Gender?> GetGenderById(int id);
        Task<List<Gender?>> GetAllGenders();
    }
}
