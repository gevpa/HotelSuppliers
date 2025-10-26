using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Suppliers.DTO;
using Suppliers.Models;
using Suppliers.Services;

namespace Suppliers.Pages.SuppliersPages
{
    public class IndexModel : PageModel
    {
        public Error? ErrorObj { get; set; }
        public IList<SupplierReadOnlyDTO> SuppliersDto { get; set; } = new List<SupplierReadOnlyDTO>();

        private readonly ISupplierService? _supplierService;
        private readonly IMapper? _mapper;

        public IndexModel(ISupplierService? supplierService, IMapper? mapper)
        {
            _supplierService = supplierService;
            _mapper = mapper;
        }

        public IActionResult OnGet()
        {
            try
            {
                ErrorObj = null;
                IList<Supplier> suppliers = _supplierService!.GetAllSuppliers();
                SuppliersDto = new List<SupplierReadOnlyDTO>();
                foreach (Supplier supplier in suppliers) 
                {
                    SupplierReadOnlyDTO? supplierDto = _mapper!.Map<SupplierReadOnlyDTO>(supplier);
                    SuppliersDto.Add(supplierDto);
                }
            }
            catch (Exception e) 
            {
                ErrorObj = new Error("", e.Message, "");
            }
            return Page();
        }
    }
}
