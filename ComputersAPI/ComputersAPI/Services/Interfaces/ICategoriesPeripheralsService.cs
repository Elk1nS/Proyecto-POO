using ComputersAPI.Dtos.CategoriesPeripherals;
using ComputersAPI.Dtos.Common;

namespace ComputersAPI.Services.Interfaces
{
    public interface ICategoriesPeripheralsService
    {
        Task<ResponseDto<CategoryPeripheralActionResponseDto>> CreateAsync(CategoryPeripheralCreateDto category);
        Task<ResponseDto<CategoryPeripheralActionResponseDto>> DeleteAsync(Guid id);
        Task<ResponseDto<CategoryPeripheralActionResponseDto>> EditAsync(CategoryPeripheralEditDto dto, Guid id);
        Task<ResponseDto<List<CategoryPeripheralDto>>> GetListAsync();
        Task<ResponseDto<CategoryPeripheralDto>> GetOneByIdAsync(Guid id);
    }
}
