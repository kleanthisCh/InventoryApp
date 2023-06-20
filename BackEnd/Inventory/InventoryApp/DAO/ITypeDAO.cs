
namespace InventoryApp.DAO
{
    public interface ITypeDAO
    {
        Models.Type Insert( Models.Type type );
        bool Update(int id, Models.Type type );
        bool Delete(int id );
        Models.Type? GetById(int id);
        List<Models.Type> GetAll();
    }
}
