using ComputersAPI.Dtos.CategoriesComponents;
using ComputersAPI.Dtos.Common;

namespace ComputersAPI.Services.Interfaces
{
    public interface ICategoriesComponentsService
    {
        Task<ResponseDto<CategoryComponentActionResponseDto>> CreateAsync(CategoryComponentCreateDto category);
        Task<ResponseDto<CategoryComponentActionResponseDto>> DeleteAsync(Guid id);
        Task<ResponseDto<CategoryComponentActionResponseDto>> EditAsync(CategoryComponentEditDto dto, Guid id);
        Task<ResponseDto<List<CategoryComponentDto>>> GetListAsync();
        Task<ResponseDto<CategoryComponentDto>> GetOneByIdAsync(Guid id);
    }
}
