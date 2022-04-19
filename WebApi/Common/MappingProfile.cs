using AutoMapper;
using WebApi.Applications.CategoryOperations.Commands.CreateCategory;
using WebApi.Applications.CategoryOperations.Queries.GetCategories;
using WebApi.Applications.CategoryOperations.Queries.GetCategoryDetail;
using WebApi.Applications.ProductOperations.Commands.CreateProduct;
using WebApi.Applications.ProductOperations.Queries.GetProductDetail;
using WebApi.Applications.ProductOperations.Queries.GetProducts;
using WebApi.Applications.ProductOperations.Queries.SearchProduct;
using WebApi.Entities;

namespace WebApi.Common
{
    public class MappingProfile : Profile // MappingProfile sınıfını AutoMapper isim uzayı altından gelen Profile sınıfından kalıtım aldırarak uygulamada AutoMapper ile neyin neye mapleneceğini bu sınıftan alacağını belirtmiş olduk.
    {
        public MappingProfile()
        {
            CreateMap<Product,ProductsViewModel>()
                                                .ForMember(dst=> dst.Size , opt=> opt.MapFrom(src=> ((SizeEnum)src.SizeId).ToString()))
                                                .ForMember(dst=> dst.CategoryName, opt=> opt.MapFrom(src=> src.Category.Name));// CreateMap<SourceObject , TargetObject>();
            CreateMap<Product,ProductDetailViewModel>()
                                                        .ForMember(dst=> dst.Size , opt=> opt.MapFrom(src=> ((SizeEnum)src.SizeId).ToString()))
                                                        .ForMember(dst=> dst.CategoryName, opt=> opt.MapFrom(src=> src.Category.Name));
            CreateMap<CreateProductModel,Product>();
            CreateMap<Product,SearchViewModel>()
                                                .ForMember(dst=> dst.Size , opt=> opt.MapFrom(src=> ((SizeEnum)src.SizeId).ToString()))
                                                .ForMember(dst=> dst.CategoryName, opt=> opt.MapFrom(src=> src.Category.Name));

            CreateMap<Category,CategoriesViewModel>();
            CreateMap<Category,CategoryDetailViewModel>();
            CreateMap<CreateCategoryModel,Category>();
        }
    }
}