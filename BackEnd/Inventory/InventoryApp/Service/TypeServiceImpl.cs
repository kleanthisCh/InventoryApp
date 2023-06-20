using AutoMapper;
using InventoryApp.DAO;
using InventoryApp.Data;
using InventoryApp.DTO;
using InventoryApp.Models;
using System.Transactions;

namespace InventoryApp.Service
{
    public class TypeServiceImpl : ITypeService
    {
        private readonly InventoryContext context;
        private readonly ITypeDAO typeDAO;
        private readonly IMapper mapper;

        public TypeServiceImpl(InventoryContext context, ITypeDAO typeDAO, IMapper mapper)
        {
            this.context = context;
            this.typeDAO = typeDAO;
            this.mapper = mapper;
        }   

        public async Task<Models.Type?> InsertType(TypeCreateDTO? typeDTO)
        {
            var typeToInsert = mapper.Map<Models.Type>(typeDTO);
            using var transaction = await context.Database.BeginTransactionAsync();
            Models.Type? returnedType = null;

            try
            {
                returnedType = typeDAO.Insert(typeToInsert);
                await context.SaveChangesAsync();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw;
            }

            return returnedType;
        }

        public async Task<bool> UpdateType(int id, TypeUpdateDTO? typeDTO)
        {
            bool updated = false;
            var typeToUpdate = mapper.Map<Models.Type>(typeDTO);
            typeToUpdate.TypeId = id;
            using var transaction = await context.Database.BeginTransactionAsync();

            try
            {
                updated = typeDAO.Update(id, typeToUpdate);
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

        public async Task<bool> DeleteType(int id)
        {
            bool deleted = false;
            using var transaction = await context.Database.BeginTransactionAsync();
            try
            {
                deleted = typeDAO.Delete(id);
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

        public async Task<Models.Type?> GetTypeById(int id)
        {
            Models.Type? searchingType;
            using var transaction = await context.Database.BeginTransactionAsync();
            searchingType = typeDAO.GetById(id);
            try
            {
                await context.SaveChangesAsync();
                transaction.Commit();
                return searchingType;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw;
            }
        }

        public async Task<List<Models.Type?>> GetAllTypes()
        {
            List<Models.Type?> types;
            using var transaction = await context.Database.BeginTransactionAsync();
            types = typeDAO.GetAll();
            try
            {
                await context.SaveChangesAsync();
                transaction.Commit();
                return types;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw;
            }
        }
        
    }
}
