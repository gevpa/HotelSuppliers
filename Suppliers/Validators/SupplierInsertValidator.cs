using FluentValidation;
using Suppliers.DTO;

namespace Suppliers.Validators
{
    public class SupplierInsertValidator : AbstractValidator<SupplierInsertDTO>
    {
        public SupplierInsertValidator()
        {
            RuleFor(s => s.Name).NotEmpty().WithMessage("The field 'Name' can't be empty")
                .Length(2, 255).WithMessage("The field 'Name' should have length 2-255 characters");

            RuleFor(s => s.AFM).NotEmpty().WithMessage("The field 'AFM' can't be empty")
                .Length(9).WithMessage("The field 'AFM' should have length 9 characters");

            RuleFor(s => s.Phone).NotEmpty().WithMessage("The field 'Phone' can't be empty")
                .Length(10).WithMessage("The field 'Phone' should have length 10 characters");

            RuleFor(s => s.Address).NotEmpty().WithMessage("The field 'Address' can't be empty")
                .Length(5, 100).WithMessage("The field 'Address' should have length 5 - 100 characters");

            RuleFor(s => s.Email).NotEmpty().WithMessage("The field 'Email' can't be empty")
                .Length(5, 50).WithMessage("The field 'Email' should have length 5-50 characters");
        }
    }
}
