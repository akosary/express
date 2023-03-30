using AutoMapper;
using Ecommerce.Models;
using Ecommerce.Validations.SupplierValidations;
using Ecommerce.ViewModels.BrandViewModels;
using Ecommerce.ViewModels.CategoryViewModel;
using Ecommerce.ViewModels.DepartmentViewModels;
using Ecommerce.ViewModels.ProductViewModel;
using Ecommerce.ViewModels.SupplierViewModels;
using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Department, IndexDepartmentVM>().ReverseMap();
            CreateMap<Department, AddDepartmentVM>().ReverseMap();
            CreateMap<Department, EditDepartmentVM>().ReverseMap();
            CreateMap<Department, DeleteDepartmentVM>().ReverseMap();

            CreateMap<Brand, IndexBrandVM>().ReverseMap();
            CreateMap<Brand, AddBrandVM>().ReverseMap();
            CreateMap<Brand, EditBrandVM>().ReverseMap();
            CreateMap<Brand, DeleteBrandVM>().ReverseMap();

            CreateMap<Supplier, IndexSupplierVM>().ReverseMap();
            CreateMap<Supplier, AddSupplierVM>().ReverseMap();
            CreateMap<Supplier, EditSupplierVM>().ReverseMap();
            CreateMap<Supplier, DeleteSupplierVM>().ReverseMap();

            CreateMap<Category, IndexCategoryVM>().ReverseMap();
            CreateMap<Category, AddCategoryVM>().ReverseMap();
            CreateMap<Category, EditCategoryVM>().ReverseMap();
            CreateMap<Category, DeleteCategoryVM>().ReverseMap();

            CreateMap<Product, IndexProductVM>().ReverseMap();
            CreateMap<Product, DetailsProductVM>().ReverseMap();
            CreateMap<Product, AddProductVM>().ReverseMap();
            CreateMap<Product, EditProductVM>().ReverseMap();
            CreateMap<Product, DeleteProductVM>().ReverseMap();
        }
    }
}
