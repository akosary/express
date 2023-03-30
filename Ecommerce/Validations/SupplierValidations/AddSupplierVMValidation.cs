using Ecommerce.ViewModels.SupplierViewModels;
using FluentValidation;

namespace Ecommerce.Validations.SuppliersValidations
{
    public class AddSupplierVMValidation : AbstractValidator<AddSupplierVM>
    {
        public AddSupplierVMValidation()
        {
            RuleFor(ads => ads.Name)
                .Cascade(RuleLevelCascadeMode)
                .NotNull().WithMessage("{PropertyName} is required")
                .NotEmpty().WithMessage("{PropertyName} can not be empty")
                .MaximumLength(100).WithMessage("Maximum Supplier {PropertyName} Character Length is 100");
            
            RuleFor(ads => ads.Zip)
                .Cascade(RuleLevelCascadeMode)
                .NotNull().WithMessage("{PropertyName} is required")
                .NotEmpty().WithMessage("{PropertyName} can not be empty")
                .MaximumLength(10).WithMessage("Maximum Supplier {PropertyName} Length is 10");

            RuleFor(ads => ads.Street)
                .Cascade(RuleLevelCascadeMode)
                .NotNull().WithMessage("{PropertyName} is required")
                .NotEmpty().WithMessage("{PropertyName} can not be empty")
                .MaximumLength(100).WithMessage("Maximum Supplier {PropertyName} Character Length is 100");

            RuleFor(ads => ads.City)
                .Cascade(RuleLevelCascadeMode)
                .NotNull().WithMessage("{PropertyName} is required")
                .NotEmpty().WithMessage("{PropertyName} can not be empty")
                .MaximumLength(100).WithMessage("Maximum Supplier {PropertyName} Character Length is 100");

            RuleFor(ads => ads.SupplierPhones[0].PhoneNumber)
                .Cascade(RuleLevelCascadeMode)
                .NotNull().WithMessage("{PropertyName} is required")
                .NotEmpty().WithMessage("{PropertyName} can not be empty")
                .Length(11).WithMessage("Supplier {PropertyName} Character Length is 11");

            RuleFor(ads => ads.SupplierPhones[1].PhoneNumber)
                .Cascade(RuleLevelCascadeMode)
                .NotNull().WithMessage("{PropertyName} is required")
                .NotEmpty().WithMessage("{PropertyName} can not be empty")
                .Length(11).WithMessage("Supplier {PropertyName} Character Length is 11");

        }
    }
}
