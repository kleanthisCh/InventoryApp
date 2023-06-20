using InventoryApp.Data;
using Microsoft.EntityFrameworkCore;

namespace InventoryApp.DAO
{
    public class TypeDAOImpl : ITypeDAO
    {
        private readonly InventoryContext _context;

        public TypeDAOImpl(InventoryContext context)
        {
            _context = context;
        }

        public Models.Type Insert(Models.Type type)
        {
            if (_context.Types == null)
            {
                return null;
            }

            _context.Types.Add(type);
            return type;
        }

        public bool Update(int id, Models.Type type)
        {
            
            if (id != type.TypeId)
            {
                return false;
            }
            var testType = GetById(id);
            if (testType == null)
            {
                return false;
            }
            _context.Types.Update(type);
            return true;
        }

        public bool Delete(int id)
        {
            if (_context.Types == null)
            {
                return false;
            }
            var testType = GetById(id);
            if (testType == null)
            {
                return false;
            }
            _context.Types.Remove(testType);
            return true;
        }

        public Models.Type? GetById(int id)
        {
            if (_context.Types == null)
            {
                return null;
            }

            Models.Type? type = _context.Types.AsNoTracking().Where(x => x.TypeId == id).FirstOrDefault();
            return type;
        }

        public List<Models.Type> GetAll()
        {
            var list = new List<Models.Type>();
            if (_context.Types == null)
            {
                return list;
            }
            var types = _context.Types.ToList();
            return types;

        }

        

        
    }
}
