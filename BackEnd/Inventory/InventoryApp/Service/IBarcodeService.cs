using InventoryApp.DTO;
using InventoryApp.Models;

namespace InventoryApp.Service
{
    public interface IBarcodeService
    {
        Task<Barcode?> InsertBarcode(BarcodeCreateDTO? barcodeDTO);
        Task<bool> UpdateBarcode(string id, BarcodeUpdateDTO? barcodeDTO);
        Task<bool> DeleteBarcode(string id);
        Task<bool> AddOne(string id);
        Task<bool> SubstractOne(string id);
        Task<Barcode?> GetBarcodeByBarcodeId(string id);
        Task<List<Barcode?>> GetBarcodesByModel(string model);
        Task<List<Barcode?>> GetAllBarcodes();
    }
}
