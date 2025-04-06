using ComputersAPI.Dtos.Common;
using ComputersAPI.Dtos.Computers;

namespace ComputersAPI.Services.Interfaces
{
    public interface IComputersService
    {
        Task<ResponseDto<ComputerActionResponseDto>> CreateAsync(ComputerCreateDto computer);
        Task<ResponseDto<ComputerActionResponseDto>> DeleteAsync(Guid id);
        Task<ResponseDto<ComputerActionResponseDto>> EditAsync(ComputerEditDto dto, Guid id);
        Task<ResponseDto<List<ComputerDto>>> GetListAsync();
        Task<ResponseDto<ComputerDto>> GetOneByIdAsync(Guid id);
    }
}
