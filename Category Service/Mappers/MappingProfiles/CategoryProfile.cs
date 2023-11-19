using AutoMapper;
using Business.Data;
using Data.Models;
using Web.Data.Models.Category;

namespace Mappers.MappingProfiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryModel>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.CategoryId))
               .ForMember(dest => dest.ParentId, opt => opt.MapFrom(src => src.ParentCategoryId))
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.CategoryName))
               .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.CategoryDescription));

            CreateMap<CategoryModel, Category>()
               .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.ParentCategoryId, opt => opt.MapFrom(src => src.ParentId))
               .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Name))
               .ForMember(dest => dest.CategoryDescription, opt => opt.MapFrom(src => src.Description));

            CreateMap<CategoryModel, CategoryViewModelOnRead>()
               .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.ParentCategoryId, opt => opt.MapFrom(src => src.ParentId))
               .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Name))
               .ForMember(dest => dest.CategoryDescription, opt => opt.MapFrom(src => src.Description));

            CreateMap<CategoryViewModelOnCreate, CategoryModel>()
               .ForMember(dest => dest.ParentId, opt => opt.MapFrom(src => src.ParentCategoryId))
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.CategoryName))
               .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.CategoryDescription));
        }
    }
}
