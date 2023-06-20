using InventoryApp.Models;

namespace InventoryApp.DAO
{
    public interface IBarcodeDAO
    {
        Barcode? Insert(Barcode barcode);
        bool Update(string id, Barcode barcode);
        bool Delete(string id);
        Barcode? GetByBarcodeId(string id);
        List<Barcode?> GetByProductId(int id);

        List<Barcode?> GetByProductModel(string model);
        List<Barcode?> GetAll();
    }
}
