using AutoMapper;
using InventoryApp.DAO;
using InventoryApp.Data;
using InventoryApp.DTO;
using InventoryApp.Models;

namespace InventoryApp.Service
{
    public class GenderServiceImpl : IGenderService
    {
        private readonly InventoryContext context;
        private readonly IGenderDAO genderDAO;
        private readonly IMapper mapper;

        public GenderServiceImpl(InventoryContext context, IGenderDAO genderDAO, IMapper mapper)
        {
            this.context = context;
            this.genderDAO = genderDAO;
            this.mapper = mapper;
        }

        public async Task<Gender?> InsertGender(GenderCreateDTO? genderDTO)
        {
            var genderToInsert = mapper.Map<Gender>(genderDTO);
            using var transaction = await context.Database.BeginTransactionAsync();
            Gender? returnedGender = null;

            try
            {
                returnedGender = genderDAO.Insert(genderToInsert);
                await context.SaveChangesAsync();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw;
            }

            return returnedGender;
        }

        public async Task<bool> UpdateGender(int id, GenderUpdateDTO? genderDTO)
        {
            bool updated = false;
            var genderToUpdate = mapper.Map<Gender>(genderDTO);
            genderToUpdate.GenderId = id;
            using var transaction = await context.Database.BeginTransactionAsync();

            try
            {
                updated = genderDAO.Update(id, genderToUpdate);
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

        public async Task<bool> DeleteGender(int id)
        {
            bool deleted = false;
            using var transaction = await context.Database.BeginTransactionAsync();
            try
            {
                deleted = genderDAO.Delete(id);
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

        public async Task<Gender?> GetGenderById(int id)
        {
            Gender? searchingGender;
            using var transaction = await context.Database.BeginTransactionAsync();
            searchingGender = genderDAO.GetById(id);
            try
            {
                await context.SaveChangesAsync();
                transaction.Commit();
                return searchingGender;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw;
            }
        }

        public async Task<List<Gender?>> GetAllGenders()
        {
            List<Gender?> genders;
            using var transaction = await context.Database.BeginTransactionAsync();
            genders = genderDAO.GetAll();
            try
            {
                await context.SaveChangesAsync();
                transaction.Commit();
                return genders;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw;
            }
        }

    }
}
