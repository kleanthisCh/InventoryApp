using InventoryApp.Data;
using InventoryApp.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryApp.DAO
{
    public class ManufacturerDAOImpl : IManufacturerDAO
    {
        private readonly InventoryContext _context;
        
        public ManufacturerDAOImpl(InventoryContext context)
        {
            this._context = context;
        }

        public Manufacturer? Insert(Manufacturer manufacturer)
        {
            if (_context.Manufacturers == null)
            {
                return null;
            }
            Console.WriteLine("DAO");
            Console.WriteLine(manufacturer.ToString());
            _context.Manufacturers.Add(manufacturer);
            return manufacturer;
        }

        public bool Update(int id, Manufacturer manufacturer)
        {
            
            if (id != manufacturer.ManufacturerId)
            {
                return false;
            }
            var testManufacturer = GetById(id);
            if (testManufacturer == null)
            {
                return false;
            }

            _context.Manufacturers.Update(manufacturer);
            return true; 

        }

        public bool Delete(int id)
        {
            if (_context.Manufacturers == null)
            {
                return false;
            }
            var manufacturer = GetById(id);
            if (manufacturer == null)
            {
                return false;
            }

            _context.Manufacturers.Remove(manufacturer);
            return true;
        }

        public Manufacturer? GetById(int id)
        {
            if (_context.Manufacturers == null)
            {
                return null;
            }

            Manufacturer? manufacturer =  _context.Manufacturers.AsNoTracking().Where(x => x.ManufacturerId == id).FirstOrDefault();
            return manufacturer;
        }

        public List<Manufacturer?> GetAll()
        {
            var list = new List<Manufacturer?>();
            if (_context.Manufacturers == null)
            {
                return list;
            }
            var manufacturers = _context.Manufacturers.ToList();
            return manufacturers;
        }
    }
}
