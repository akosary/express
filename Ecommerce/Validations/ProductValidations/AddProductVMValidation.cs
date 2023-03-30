using Ecommerce.ViewModels.ProductViewModel;
using FluentValidation;

namespace Ecommerce.Validations.ProductValidations
{
    public class AddProductVMValidation : AbstractValidator<AddProductVM>
    {
        public AddProductVMValidation()
        {
            RuleFor(ap => ap.Name)
               .Cascade(RuleLevelCascadeMode)
               .NotNull().WithMessage("{PropertyName} is required")
               .NotEmpty().WithMessage("{PropertyName} can not be empty")
               .MaximumLength(100).WithMessage("Maximum Department {PropertyName} Character Length is 100").WithName("Image");

            RuleFor(ap => ap.Description)
               .Cascade(RuleLevelCascadeMode)
               .NotNull().WithMessage("{PropertyName} is required")
               .NotEmpty().WithMessage("{PropertyName} can not be empty");

            RuleFor(ap => ap.CoverImgPath)
                .MaximumLength(256)
                .OverridePropertyName("Image");

            RuleFor(ap => ap.ImgFile)
                .Cascade(RuleLevelCascadeMode)
                .NotNull().WithMessage("Image is required")
                .NotEmpty().WithMessage("Image can not be empty");

            RuleFor(ap => ap.CategoryId)
                .NotNull().WithMessage("Select a category")
                .NotEmpty().WithMessage("Select a category");

            RuleFor(ap => ap.BrandId)
                .NotNull().WithMessage("Select a brand")
                .NotEmpty().WithMessage("Select a brand");

            RuleFor(ap => ap.SupplierId)
                .NotNull().WithMessage("Select a supplier")
                .NotEmpty().WithMessage("Select a supplier");

            RuleFor(ap => ap.Stock)
                .NotNull().WithMessage("{PropertyName} is required")
                .NotEmpty().WithMessage("{PropertyName} can not be empty");

            RuleFor(ap => ap.UnitPrice)
                .NotNull().WithMessage("{PropertyName} is required")
                .NotEmpty().WithMessage("{PropertyName} can not be empty");

            RuleFor(ap => ap.ImgFileMultiple)
                .Cascade(RuleLevelCascadeMode)
                .NotNull().WithMessage("Image is required")
                .NotEmpty().WithMessage("Image can not be empty");


        }
    }
}
