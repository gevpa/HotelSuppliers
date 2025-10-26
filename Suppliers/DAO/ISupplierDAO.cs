using Suppliers.Models;
namespace Suppliers.DAO
{
    public interface ISupplierDAO
    {
        Supplier? Insert(Supplier suppliers);
        Supplier? Update(Supplier? suppliers);
        void Delete(int id);
        Supplier? GetById(int id);
        IList<Supplier> GetAll();
        Supplier? GetById(object id);
    }
}
