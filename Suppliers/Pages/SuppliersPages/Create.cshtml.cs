using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Suppliers.DTO;
using Suppliers.Models;
using Suppliers.Services;

namespace Suppliers.Pages.SuppliersPages
{
    public class CreateModel : PageModel
    {
        public List<Error> ErrorArray { get; set; } = new();
        public SupplierInsertDTO SupplierInsertDTO { get; set; } = new();

        private readonly ISupplierService? _supplierService;
        private readonly IValidator<SupplierInsertDTO> _supplierInsertvalidator;

        public CreateModel(ISupplierService? supplierService, IValidator<SupplierInsertDTO> supplierInsertvalidator)
        {
            _supplierService = supplierService;
            _supplierInsertvalidator = supplierInsertvalidator;
        }

        public void OnGet()
        {
        }

        public void OnPost(SupplierInsertDTO dto) 
        {
            //When Submit clicked and Page Refreshes the text-boxes retain the old values through SupplierInsertDto
            SupplierInsertDTO = dto;

            var validationResult = _supplierInsertvalidator.Validate(dto);
            if(!validationResult.IsValid)
            {
                foreach(var error in validationResult.Errors)
                {
                    ErrorArray!.Add(new Error (error.ErrorCode, error.ErrorMessage, error.PropertyName));
                }
                return;
            }

            try
            {
                Supplier supplier = _supplierService?.InsertSupplier(dto)!;
                Response.Redirect("/SuppliersPages/getall");
            }
            catch (Exception e)
            {
                ErrorArray!.Add(new Error("", e.Message, ""));
            }

        }
    }
}
