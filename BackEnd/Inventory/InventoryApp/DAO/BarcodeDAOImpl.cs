using InventoryApp.Data;
using InventoryApp.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryApp.DAO
{
    public class BarcodeDAOImpl : IBarcodeDAO
    {
        private readonly InventoryContext _context;
        public BarcodeDAOImpl(InventoryContext context)
        {
            this._context = context;
        }

        public Barcode? Insert(Barcode barcode)
        {
            if (_context.Barcodes == null)
            {
                return null;
            }

            _context.Barcodes.Add(barcode);
            return barcode;
        }

        public bool Update(string id, Barcode barcode)
        {
            if (id != barcode.BarcodeId)
            {
                return false;
            }
            var testBarcode = GetByBarcodeId(id);
            if (testBarcode == null)
            {
                return false;
            }

            _context.Barcodes.Update(barcode);
            return true;
        }

        public bool Delete(string id)
        {
            if (_context.Barcodes == null)
            {
                return false;
            }
            var barcode = GetByBarcodeId(id);
            if (barcode == null)
            {
                return false;
            }

            _context.Barcodes.Remove(barcode);
            return true;
        }

        public Barcode? GetByBarcodeId(string id)
        {
            if (_context.Barcodes == null)
            {
                return null;
            }

            Barcode? barcode = _context.Barcodes.AsNoTracking().Where(x => x.BarcodeId == id).FirstOrDefault();
            return barcode;
        }

        public List<Barcode?> GetByProductId(int id)
        {
            var list = new List<Barcode?>();
            if (_context.Barcodes == null)
            {
                return list;
            }
            list = _context.Barcodes.AsNoTracking().Where(x => x.ProductId == id).ToList();
            return list;
        }

        public List<Barcode?> GetByProductModel(string model)
        {
            var list = new List<Barcode?>();
            if (_context.Barcodes == null)
            {
                return list;
            }
            list = _context.Barcodes.AsNoTracking().Where(x => x.Model == model).ToList();
            return list;
        }

        public List<Barcode?> GetAll()
        {
            var list = new List<Barcode?>();
            if (_context.Barcodes == null)
            {
                return list;
            }
            var barcodes = _context.Barcodes.ToList();
            return barcodes;
        }

        

        
    }
}
