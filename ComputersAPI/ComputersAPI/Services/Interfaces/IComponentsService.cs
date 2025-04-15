using ComputersAPI.Dtos.Common;
using ComputersAPI.Dtos.Components;

namespace ComputersAPI.Services.Interfaces
{
    public interface IComponentsService
    {
        Task<ResponseDto<ComponentActionResponseDto>> CreateAsync(ComponentCreateDto component);
        Task<ResponseDto<ComponentActionResponseDto>> DeleteAsync(Guid id);
        Task<ResponseDto<ComponentActionResponseDto>> EditAsync(ComponentEditDto dto, Guid id);
        Task<ResponseDto<List<ComponentDto>>> GetListAsync();
        Task<ResponseDto<ComponentDto>> GetOneByIdAsync(Guid id);
    }
}
