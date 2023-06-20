using InventoryApp.Data;
using InventoryApp.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryApp.DAO
{
    public class GenderDAOImpl : IGenderDAO
    {
        private readonly InventoryContext _context;

        public GenderDAOImpl (InventoryContext context)
        {
            _context = context;
        }

        public Gender? Insert(Gender gender)
        {
            if (_context.Genders == null)
            {
                return null;
            }

            _context.Genders.Add(gender);
            return gender;
        }

        public bool Update(int id, Gender gender)
        {
            if (id != gender.GenderId)
            {
                return false;
            }
            var testGender = GetById(id);
            if (testGender == null)
            {
                return false;
            }
            _context.Genders.Update(gender);
            return true;
        }

        public bool Delete(int id)
        {
            if (_context.Genders == null)
            {
                return false;
            }
            var testGender = GetById(id);
            if (testGender == null)
            {
                return false;
            }
            _context.Genders.Remove(testGender);
            return true;
        }

        public Gender? GetById(int id)
        {
            if (_context.Genders == null)
            {
                return null;
            }

            Gender? gender = _context.Genders.AsNoTracking().Where(x => x.GenderId == id).FirstOrDefault();
            return gender;
        }

        public List<Gender?> GetAll()
        {
            var list = new List<Gender>();
            if (_context.Genders == null)
            {
                return list;
            }
            var genders = _context.Genders.ToList();
            return genders;
        }

    }
}
