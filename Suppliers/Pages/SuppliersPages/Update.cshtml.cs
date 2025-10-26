using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Suppliers.DTO;
using Suppliers.Models;
using Suppliers.Services;
using Suppliers.Validators;

namespace Suppliers.Pages.SuppliersPages
{
    public class UpdateModel : PageModel
    {
        public SupplierUpdateDTO SupplierUpdateDto { get; set; } = new();
        public List<Error> ErrorArray { get; set; } = new();

        private readonly ISupplierService _supplierService;
        private readonly IMapper _mapper;
        private readonly IValidator<SupplierUpdateDTO> _supplierUpdateValidator;

        public UpdateModel(ISupplierService supplierService, IMapper mapper, IValidator<SupplierUpdateDTO> supplierUpdateValidator)
        {
            _supplierService = supplierService;
            _mapper = mapper;
            _supplierUpdateValidator = supplierUpdateValidator;
        }

        public IActionResult OnGet(int SupplierId)
        {
            try
            {
                Supplier? supplier = _supplierService.GetSupplier(SupplierId);
                SupplierUpdateDto = _mapper.Map<SupplierUpdateDTO>(supplier);
            }
            catch (Exception e)
            {
                ErrorArray.Add(new Error("", e.Message, ""));
            }

            return Page();
        }
        

        public void OnPost(SupplierUpdateDTO dto)
        {
            //Refresh!
            SupplierUpdateDto = dto;

            var validationResult = _supplierUpdateValidator.Validate(dto);
            if (!validationResult.IsValid)
            {
                ErrorArray = new();
                foreach (var error in validationResult.Errors)
                {
                    ErrorArray.Add(new Error(error.ErrorCode, error.ErrorMessage, error.PropertyName));
                }
                return;
            }

            try
            {
                Supplier? supplier = _supplierService.UpdateSupplier(dto);
                Response.Redirect("/SuppliersPages/getall");
            }
            catch (Exception e)
            {
                ErrorArray.Add(new Error("Update", e.Message, e.ToString()));
            }

        }


    }
}
