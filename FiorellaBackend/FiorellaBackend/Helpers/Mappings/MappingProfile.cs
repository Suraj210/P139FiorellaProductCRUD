using AutoMapper;
using FiorellaBackend.Areas.Admin.ViewModels.Category;
using FiorellaBackend.Areas.Admin.ViewModels.Product;
using FiorellaBackend.Models;

namespace FiorellaBackend.Helpers.Mappings
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductVM>().ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                                           .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Images.FirstOrDefault(m=>m.IsMain).Image));


            CreateMap<Category, CategoryVM>();
        }
    }
}
