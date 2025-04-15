using AutoMapper;
using ComputersAPI.Constants;
using ComputersAPI.Database;
using ComputersAPI.Database.Entities;
using ComputersAPI.Dtos.CategoriesComponents;
using ComputersAPI.Dtos.Common;
using ComputersAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ComputersAPI.Services
{
    public class CategoriesComponentsService : ICategoriesComponentsService
    {
        private readonly ComputersDbContext _context;
        private readonly IMapper _mapper;

        public CategoriesComponentsService(ComputersDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResponseDto<List<CategoryComponentDto>>> GetListAsync()
        {
            var categoriesEntity = await _context.CategoriesComponents.ToListAsync();

            var categoriesDto = _mapper.Map<List<CategoryComponentDto>>(categoriesEntity);

            return new ResponseDto<List<CategoryComponentDto>>
            {
                StatusCode = HttpStatusCode.Ok,
                Status = true,
                Message = categoriesEntity.Count() > 0 ? "Registros encontrados" : "No se encontraron registros",
                Data = categoriesDto
            };
        }
        public async Task<ResponseDto<CategoryComponentDto>> GetOneByIdAsync(Guid id)
        {
            var categoryEntity = await _context.CategoriesComponents.FirstOrDefaultAsync(x => x.Id == id);

            if (categoryEntity is null)
            {
                return new ResponseDto<CategoryComponentDto>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Status = false,
                    Message = "Registro no encontrado"
                };
            }

            return new ResponseDto<CategoryComponentDto>
            {
                StatusCode = HttpStatusCode.Ok,
                Status = true,
                Message = "Registro encontrado",
                Data = _mapper.Map<CategoryComponentDto>(categoryEntity)
            };
        }
        public async Task<ResponseDto<CategoryComponentActionResponseDto>> CreateAsync(CategoryComponentCreateDto dto)
        {

            var categoryEntity = _mapper.Map<CategoryComponentEntity>(dto);

            _context.CategoriesComponents.Add(categoryEntity);
            await _context.SaveChangesAsync();

            return new ResponseDto<CategoryComponentActionResponseDto>
            {
                StatusCode = HttpStatusCode.CREATED,
                Status = true,
                Message = "Registro creado correctamente",
                Data = _mapper.Map<CategoryComponentActionResponseDto>(categoryEntity)
            };
        }
        public async Task<ResponseDto<CategoryComponentActionResponseDto>> EditAsync(CategoryComponentEditDto dto, Guid id)
        {
            var categoryEntity = await _context.CategoriesComponents.FirstOrDefaultAsync(x => x.Id == id);

            if (categoryEntity is null)
            {
                return new ResponseDto<CategoryComponentActionResponseDto>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Status = false,
                    Message = "Registro no encontrado",
                };
            }

            _mapper.Map<CategoryComponentEditDto, CategoryComponentEntity>(dto, categoryEntity);

            _context.CategoriesComponents.Update(categoryEntity);
            await _context.SaveChangesAsync();

            return new ResponseDto<CategoryComponentActionResponseDto>
            {
                StatusCode = HttpStatusCode.Ok,
                Status = true,
                Message = "Registro editado correctamente",
                Data = _mapper.Map<CategoryComponentActionResponseDto>(categoryEntity)
            };
        }
        public async Task<ResponseDto<CategoryComponentActionResponseDto>> DeleteAsync(Guid id)
        {
            var categoryEntity = await _context.CategoriesComponents.FirstOrDefaultAsync(x => x.Id == id);

            if (categoryEntity is null)
            {
                return new ResponseDto<CategoryComponentActionResponseDto>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Status = false,
                    Message = "Registro no encontrado",
                };
            }

            _context.CategoriesComponents.Remove(categoryEntity);
            await _context.SaveChangesAsync();

            return new ResponseDto<CategoryComponentActionResponseDto>
            {
                StatusCode = HttpStatusCode.Ok,
                Status = true,
                Message = "Registro eliminado Correctamente",
                Data = _mapper.Map<CategoryComponentActionResponseDto>(categoryEntity)
            };
        }

    }
}