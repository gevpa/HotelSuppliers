using Suppliers.DTO;
using Suppliers.Models;

namespace Suppliers.Services
{
    public interface ISupplierService
    {
        IList<Supplier> GetAllSuppliers();
        Supplier? GetSupplier(int id);
        Supplier? InsertSupplier(SupplierInsertDTO dto);
        Supplier? UpdateSupplier(SupplierUpdateDTO dto);
        Supplier? DeleteSupplier(int id);
    }
}
