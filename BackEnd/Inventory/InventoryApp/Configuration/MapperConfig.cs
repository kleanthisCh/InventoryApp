using AutoMapper;
using InventoryApp.DTO;
using InventoryApp.Models;

namespace InventoryApp.Configuration
{
    public class MapperConfig :Profile
    {
        public MapperConfig() 
        {
            CreateMap<ManufacturerCreateDTO, Manufacturer>().ReverseMap();
            CreateMap<ManufacturerReadOnlyDTO, Manufacturer>().ReverseMap();
            CreateMap<ManufacturerUpdateDTO, Manufacturer>().ReverseMap();

            CreateMap<TypeCreateDTO, Models.Type>().ReverseMap();
            CreateMap<TypeReadOnlyDTO, Models.Type>().ReverseMap();
            CreateMap<TypeUpdateDTO, Models.Type>().ReverseMap();

            CreateMap<GenderCreateDTO, Gender>().ReverseMap();
            CreateMap<GenderReadOnlyDTO, Gender>().ReverseMap();
            CreateMap<GenderUpdateDTO, Gender>().ReverseMap();

            CreateMap<ProductCreateDTO, Product>().ReverseMap();
            CreateMap<Product, ProductReadOnlyDTO>().ForMember(dest => dest.ManufacturerName, act => act.MapFrom(src => src.Manufacturer.ManufacturerName))
                                                    .ForMember(dest => dest.GenderDescription, act => act.MapFrom(src => src.Gender.GenderDescription))
                                                    .ForMember(dest => dest.TypeDescription, act => act.MapFrom(src => src.Type.TypeDescription)).ReverseMap();
            CreateMap<ProductUpdateDTO, Product>().ReverseMap();

            CreateMap<BarcodeCreateDTO, Barcode>().ReverseMap();
            CreateMap<BarcodeReadOnlyDTO, Barcode>().ReverseMap();
            CreateMap<BarcodeUpdateDTO, Barcode>().ReverseMap();
        }
    }
}
