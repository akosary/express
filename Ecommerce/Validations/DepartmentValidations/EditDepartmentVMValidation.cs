using Ecommerce.ViewModels.DepartmentViewModels;
using FluentValidation;

namespace Ecommerce.Validations.DepartmentValidations
{
    public class EditDepartmentVMValidation : AbstractValidator<EditDepartmentVM>
    {
        public EditDepartmentVMValidation()
        {
            RuleFor(ed => ed.Name)
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
        }
    }
}
