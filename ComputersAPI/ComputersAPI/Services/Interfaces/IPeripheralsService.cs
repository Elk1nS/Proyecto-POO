using ComputersAPI.Dtos.Common;
using ComputersAPI.Dtos.Peripherals;

namespace ComputersAPI.Services.Interfaces
{
    public interface IPeripheralsService
    {
        Task<ResponseDto<PeripheralActionResponseDto>> CreateAsync(PeripheralCreateDto peripheral);
        Task<ResponseDto<PeripheralActionResponseDto>> DeleteAsync(Guid id);
        Task<ResponseDto<PeripheralActionResponseDto>> EditAsync(PeripheralEditDto dto, Guid id);
        Task<ResponseDto<List<PeripheralDto>>> GetListAsync();
        Task<ResponseDto<PeripheralDto>> GetOneByIdAsync(Guid id);
    }
}
