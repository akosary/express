using Ecommerce.ViewModels.BrandViewModels;
using FluentValidation;

namespace Ecommerce.Validations.BrandValidations
{
    public class AddBrandVMValidation : AbstractValidator<AddBrandVM>
    {
        public AddBrandVMValidation()
        {
            RuleFor(ab => ab.Name)
                .Cascade(RuleLevelCascadeMode)
                .NotNull().WithMessage("{PropertyName} is required")
                .NotEmpty().WithMessage("{PropertyName} can not be empty")
                .MaximumLength(100).WithMessage("Maximum Department {PropertyName} Character Length is 100").WithName("Image");

            RuleFor(ab => ab.ImgPath)
                .MaximumLength(256)
                .OverridePropertyName("Image");

        }
    }
}
