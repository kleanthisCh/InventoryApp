using AutoMapper;
using InventoryApp.DAO;
using InventoryApp.Data;
using InventoryApp.DTO;
using InventoryApp.Models;

namespace InventoryApp.Service
{
    public class ProductServiceImpl : IProductService
    {

        private readonly InventoryContext context;
        private readonly IProductDAO productDAO;
        private readonly IBarcodeDAO barcodeDAO;
        private readonly IManufacturerDAO manufacturerDAO;
        private readonly IGenderDAO genderDAO;
        private readonly ITypeDAO typeDAO;
        private readonly IMapper mapper;

        public ProductServiceImpl (InventoryContext context, IProductDAO productDAO, IMapper mapper, IBarcodeDAO barcodeDAO, 
            IManufacturerDAO manufacturerDAO, IGenderDAO genderDAO, ITypeDAO typeDAO)
        {
            this.context = context;
            this.productDAO = productDAO;
            this.mapper = mapper;
            this.barcodeDAO = barcodeDAO;
            this.manufacturerDAO = manufacturerDAO;
            this.genderDAO = genderDAO;
            this.typeDAO = typeDAO;
        }



        public async Task<Product?> InsertProduct(ProductCreateDTO productDTO)
        {
            var product = mapper.Map<Product>(productDTO);

            using var transaction = await context.Database.BeginTransactionAsync();
            Product? returnedProduct = null;

            try
            {
                returnedProduct = productDAO.Insert(product);
                await context.SaveChangesAsync();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw;
            }

            return returnedProduct;
        }

        public async Task<bool> UpdateProduct(int id, ProductUpdateDTO productDTO)
        {
            bool updated = false;
            var product = mapper.Map<Product>(productDTO);
            product.ProductId = id;
            using var transaction = await context.Database.BeginTransactionAsync();

            try
            {
                updated = productDAO.Update(id, product);
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

        public async Task<bool> DeleteProduct(int id)
        {
            bool deleted = false;
            using var transaction = await context.Database.BeginTransactionAsync();
            try
            {
                deleted = productDAO.Delete(id);
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

        public async Task<Product?> GetProductById(int id)
        {
            Product? product;
            using var transaction = await context.Database.BeginTransactionAsync();
            product = productDAO.GetById(id);
            if (product is not null)
            {
                product.Barcodes = barcodeDAO.GetByProductModel(product.Model);
                product.Manufacturer = manufacturerDAO.GetById(product.ManufacturerId);
                product.Gender = genderDAO.GetById(product.GenderId);
                product.Type = typeDAO.GetById(product.TypeId);
            }
            try
            {
                await context.SaveChangesAsync();
                transaction.Commit();
                return product;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw;
            }
        }

        public async Task<Product?> GetProductByName(string name)
        {
            Product? product;
            using var transaction = await context.Database.BeginTransactionAsync();
            product = productDAO.GetByModel(name);
            if (product is not null)
            {
                product.Barcodes = barcodeDAO.GetByProductModel(product.Model);
                product.Manufacturer = manufacturerDAO.GetById(product.ManufacturerId);
                product.Gender = genderDAO.GetById(product.GenderId);
                product.Type = typeDAO.GetById(product.TypeId);
            }
            try
            {
                await context.SaveChangesAsync();
                transaction.Commit();
                return product;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw;
            }
        }

        public async Task<List<Product>> GetAllProducts()
        {
            List<Product?> products;
            products = productDAO.GetAll();
            foreach (Product product in products)
            {
                if (product is not null)
                {
                    product.Barcodes = barcodeDAO.GetByProductModel(product.Model);
                    product.Manufacturer = manufacturerDAO.GetById(product.ManufacturerId);
                    product.Gender = genderDAO.GetById(product.GenderId);
                    product.Type = typeDAO.GetById(product.TypeId);
                }
            }
            try
            {
                return products;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        

    }
}
