using AutoMapper;
using InventoryApp.DAO;
using InventoryApp.Data;
using InventoryApp.DTO;
using InventoryApp.Models;

namespace InventoryApp.Service
{
    public class BarcodeServiceImpl : IBarcodeService
    {
        private readonly InventoryContext context;
        private readonly IBarcodeDAO barcodeDAO;
        private readonly IMapper mapper;

        public BarcodeServiceImpl(InventoryContext context, IBarcodeDAO barcodeDAO, IMapper mapper)
        {
            this.context = context;
            this.barcodeDAO = barcodeDAO;
            this.mapper = mapper;
        }

        public async Task<Barcode?> InsertBarcode(BarcodeCreateDTO? barcodeDTO)
        {
            var barcode = mapper.Map<Barcode>(barcodeDTO);

            using var transaction = await context.Database.BeginTransactionAsync();
            Barcode? returnedBarcode = null;

            try
            {
                returnedBarcode = barcodeDAO.Insert(barcode);
                await context.SaveChangesAsync();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw;
            }

            return returnedBarcode;
        }

        public async Task<bool> UpdateBarcode(string id, BarcodeUpdateDTO? barcodeDTO)
        {
            bool updated = false;
            var barcode = mapper.Map<Barcode>(barcodeDTO);
            barcode.BarcodeId = id;
            using var transaction = await context.Database.BeginTransactionAsync();

            try
            {
                updated = barcodeDAO.Update(id, barcode);
                if (updated)
                {
                    await context.SaveChangesAsync();
                    transaction.Commit();
                }
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw;
            }

            return updated;
        }

        public async Task<bool> DeleteBarcode(string id)
        {
            bool deleted = false;
            using var transaction = await context.Database.BeginTransactionAsync();
            try
            {
                deleted = barcodeDAO.Delete(id);
                if (deleted)
                {
                    await context.SaveChangesAsync();
                    transaction.Commit();
                }
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw;
            }

            return deleted;
        }

        public async Task<bool> AddOne(string id)
        {
            Barcode? barcode;
            using var transaction = await context.Database.BeginTransactionAsync();
            barcode = barcodeDAO.GetByBarcodeId(id);
            try
            {
                if (barcode == null)
                {
                    return false;
                }
                if (barcode.Quantity < 0 || barcode.Quantity >= 100)
                {
                    return false;
                }
                barcode.Quantity = barcode.Quantity + 1;
                bool updated = barcodeDAO.Update(id, barcode);
                await context.SaveChangesAsync();
                transaction.Commit();
                return updated;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw;
            }
        }

        public async Task<bool> SubstractOne(string id)
        {
            Barcode? barcode;
            using var transaction = await context.Database.BeginTransactionAsync();
            barcode = barcodeDAO.GetByBarcodeId(id);
            try
            {
                if (barcode == null)
                {
                    return false;
                }
                if (barcode.Quantity <= 0)
                {
                    return false;
                }
                barcode.Quantity = barcode.Quantity - 1;
                bool updated = barcodeDAO.Update(id, barcode);
                await context.SaveChangesAsync();
                transaction.Commit();
                return updated;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw;
            }
        }

        public async Task<Barcode?> GetBarcodeByBarcodeId(string id)
        {
            Barcode? barcode;
            using var transaction = await context.Database.BeginTransactionAsync();
            barcode = barcodeDAO.GetByBarcodeId(id);
            try
            {
                await context.SaveChangesAsync();
                transaction.Commit();
                return barcode;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw;
            }
        }

        public async Task<List<Barcode?>> GetBarcodesByModel(string model)
        {
            List<Barcode?> barcodes;
            using var transaction = await context.Database.BeginTransactionAsync();
            barcodes = barcodeDAO.GetByProductModel(model);
            try
            {
                await context.SaveChangesAsync();
                transaction.Commit();
                return barcodes;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw;
            }
        }

        public async Task<List<Barcode?>> GetAllBarcodes()
        {
            List<Barcode?> barcodes;
            using var transaction = await context.Database.BeginTransactionAsync();
            barcodes = barcodeDAO.GetAll();
            try
            {
                await context.SaveChangesAsync();
                transaction.Commit();
                return barcodes;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw;
            }
        }
    }
}
