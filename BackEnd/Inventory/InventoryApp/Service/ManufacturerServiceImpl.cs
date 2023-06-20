using AutoMapper;
using InventoryApp.DAO;
using InventoryApp.Data;
using InventoryApp.DTO;
using InventoryApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Transactions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace InventoryApp.Service
{
    public class ManufacturerServiceImpl : IManufacturerService
    {
        private readonly InventoryContext context;
        private readonly IManufacturerDAO manufacturerDAO;
        private readonly IMapper mapper;

        public ManufacturerServiceImpl(InventoryContext context, IManufacturerDAO manufacturerDAO, IMapper mapper)
        {
            this.context = context;
            this.manufacturerDAO = manufacturerDAO; 
            this.mapper = mapper;
        }


        public async Task<Manufacturer?> InsertManufacturer(ManufacturerCreateDTO? manufacturerDTO)
        {

            
            var manufacturer = mapper.Map<Manufacturer>(manufacturerDTO);
            
            using var transaction = await context.Database.BeginTransactionAsync();
            Manufacturer? returnedManufacturer = null;

            try
            {
                returnedManufacturer = manufacturerDAO.Insert(manufacturer);
                await context.SaveChangesAsync();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw;
            }

            return returnedManufacturer;
        }

        public async Task<bool> UpdateManufacturer(int id, ManufacturerUpdateDTO? manufacturerDTO)
        {
            bool updated = false;
            var manufacturer = mapper.Map<Manufacturer>(manufacturerDTO);
            manufacturer.ManufacturerId = id;
            using var transaction = await context.Database.BeginTransactionAsync();

            try
            {
                updated = manufacturerDAO.Update(id,manufacturer);
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

        public async Task<bool> DeleteManufacturer(int id)
        {
            bool deleted = false;
            using var transaction = await context.Database.BeginTransactionAsync();
            try
            {
                deleted = manufacturerDAO.Delete(id);
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

        public async Task<Manufacturer?> GetManufacturerById(int id)
        {
            Manufacturer? manufacturer;
            using var transaction = await context.Database.BeginTransactionAsync();
            manufacturer = manufacturerDAO.GetById(id);
            try {
                await context.SaveChangesAsync();
                transaction.Commit();
                return manufacturer;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw;
            }
        }

        public async Task<List<Manufacturer?>> GetAllManufacturers()
        {
            List<Manufacturer?> manufacturers;
            using var transaction = await context.Database.BeginTransactionAsync();
            manufacturers = manufacturerDAO.GetAll();
            try
            {
                await context.SaveChangesAsync();
                transaction.Commit();
                return manufacturers;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw;
            }
        }



    }
}



