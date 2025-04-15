using AutoMapper;
using ComputersAPI.Database.Entities;
using ComputersAPI.Dtos.CategoriesComponents;
using ComputersAPI.Dtos.CategoriesPeripherals;
using ComputersAPI.Dtos.Components;
using ComputersAPI.Dtos.Computers;
using ComputersAPI.Dtos.Peripherals;

namespace ComputersAPI.Helpers
{
    public class AutoMapperProfiles : Profile
    {

        public AutoMapperProfiles()
        {
            CreateMap<ComputerEntity, ComputerDto>()
    .ForMember(dest => dest.Components, opt => opt.MapFrom(src => src.ComputerComponents.Select(cc => cc.Component)))
    .ForMember(dest => dest.Peripherals, opt => opt.MapFrom(src => src.ComputerPeripherals.Select(cp => cp.Peripheral)));

            CreateMap<ComputerEntity, ComputerActionResponseDto>()
    .ForMember(dest => dest.Components, opt => opt.MapFrom(src => src.ComputerComponents.Select(cc => cc.Component)))
    .ForMember(dest => dest.Peripherals, opt => opt.MapFrom(src => src.ComputerPeripherals.Select(cp => cp.Peripheral)));

            CreateMap<ComputerCreateDto, ComputerEntity>();
            CreateMap<ComputerEditDto, ComputerEntity>();

            CreateMap<ComponentEntity, ComponentDto>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.CategoryComponent));
            CreateMap<ComponentEntity, ComponentActionResponseDto>();
            CreateMap<ComponentCreateDto, ComponentEntity>();
            CreateMap<ComponentEditDto, ComponentEntity>();

            CreateMap<CategoryComponentEntity, CategoryComponentDto>();
            CreateMap<CategoryComponentEntity, CategoryComponentActionResponseDto>();
            CreateMap<CategoryComponentCreateDto, CategoryComponentEntity>();
            CreateMap<CategoryComponentEditDto, CategoryComponentEntity>();

            CreateMap<PeripheralEntity, PeripheralDto>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.CategoryPeripheral));
            CreateMap<PeripheralEntity, PeripheralActionResponseDto>();
            CreateMap<PeripheralCreateDto, PeripheralEntity>();
            CreateMap<PeripheralEditDto, PeripheralEntity>();

            CreateMap<CategoryPeripheralEntity, CategoryPeripheralDto>();
            CreateMap<CategoryPeripheralEntity, CategoryPeripheralActionResponseDto>();
            CreateMap<CategoryPeripheralCreateDto, CategoryPeripheralEntity>();
            CreateMap<CategoryPeripheralEditDto, CategoryPeripheralEntity>();
        }
    }
}
