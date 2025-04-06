using AutoMapper;
using ComputersAPI.Database.Entities;
using ComputersAPI.Dtos.Computers;

namespace ComputersAPI.Helpers
{
    public class AutoMapperProfiles : Profile
    {

        public AutoMapperProfiles()
        {
            CreateMap<ComputerEntity, ComputerDto>();
            CreateMap<ComputerEntity, ComputerActionResponseDto>();
            CreateMap<ComputerCreateDto, ComputerEntity>();
            CreateMap<ComputerEditDto, ComputerEntity>();
        }
    }
}
