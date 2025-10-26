using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Suppliers.Models;
using Suppliers.Services;

namespace Suppliers.Pages.SuppliersPages
{
    public class DeleteModel : PageModel
    {

        public List<Error> ErrorArray { get; set; } = new();

        private readonly ISupplierService _supplierService;

        public DeleteModel(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        public void OnGet(int SupplierId)
        {
            try
            {
                Supplier? supplier = _supplierService?.DeleteSupplier(SupplierId);
                Response.Redirect("/SuppliersPages/getall");
            }catch (Exception e)
            {
                ErrorArray.Add(new Error("", e.Message, ""));
            }
        }
    }
}
