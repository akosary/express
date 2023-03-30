using Ecommerce.ViewModels.DepartmentViewModels;
using FluentValidation;

namespace Ecommerce.Validations.DepartmentValidations
{
    public class AddDepartmentVMValidation : AbstractValidator<AddDepartmentVM>
    {
        public AddDepartmentVMValidation()
        {
            RuleFor(ad => ad.Name)
                .Cascade(RuleLevelCascadeMode)
                .NotNull().WithMessage("{PropertyName} is required")
                .NotEmpty().WithMessage("{PropertyName} can not be empty")
                .MaximumLength(100).WithMessage("Maximum Department {PropertyName} Character Length is 100").WithName("Image");

            RuleFor(ad => ad.Description)
               .Cascade(RuleLevelCascadeMode)
               .NotNull().WithMessage("{PropertyName} is required")
               .NotEmpty().WithMessage("{PropertyName} can not be empty");

            RuleFor(ad => ad.ImgPath)
                .MaximumLength(256)
                .OverridePropertyName("Image");

            RuleFor(ad => ad.ImgFile)
                .Cascade(RuleLevelCascadeMode)
                .NotNull().WithMessage("Image is required")
                .NotEmpty().WithMessage("Image can not be empty");
        }
    }
}
