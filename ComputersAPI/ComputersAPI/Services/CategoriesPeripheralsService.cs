using AutoMapper;
using ComputersAPI.Constants;
using ComputersAPI.Database;
using ComputersAPI.Database.Entities;
using ComputersAPI.Dtos.CategoriesPeripherals;
using ComputersAPI.Dtos.Common;
using ComputersAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ComputersAPI.Services
{
    public class CategoriesPeripheralsService : ICategoriesPeripheralsService
    {
        private readonly ComputersDbContext _context;
        private readonly IMapper _mapper;

        public CategoriesPeripheralsService(ComputersDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResponseDto<List<CategoryPeripheralDto>>> GetListAsync()
        {
            var categoriesEntity = await _context.CategoriesPeripherals.ToListAsync();

            var categoriesDto = _mapper.Map<List<CategoryPeripheralDto>>(categoriesEntity);

            return new ResponseDto<List<CategoryPeripheralDto>>
            {
                StatusCode = HttpStatusCode.Ok,
                Status = true,
                Message = categoriesEntity.Count() > 0 ? "Registros encontrados" : "No se encontraron registros",
                Data = categoriesDto
            };
        }
        public async Task<ResponseDto<CategoryPeripheralDto>> GetOneByIdAsync(Guid id)
        {
            var categoryEntity = await _context.CategoriesPeripherals.FirstOrDefaultAsync(x => x.Id == id);

            if (categoryEntity is null)
            {
                return new ResponseDto<CategoryPeripheralDto>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Status = false,
                    Message = "Registro no encontrado"
                };
            }

            return new ResponseDto<CategoryPeripheralDto>
            {
                StatusCode = HttpStatusCode.Ok,
                Status = true,
                Message = "Registro encontrado",
                Data = _mapper.Map<CategoryPeripheralDto>(categoryEntity)
            };
        }
        public async Task<ResponseDto<CategoryPeripheralActionResponseDto>> CreateAsync(CategoryPeripheralCreateDto dto)
        {

            var categoryEntity = _mapper.Map<CategoryPeripheralEntity>(dto);

            _context.CategoriesPeripherals.Add(categoryEntity);
            await _context.SaveChangesAsync();

            return new ResponseDto<CategoryPeripheralActionResponseDto>
            {
                StatusCode = HttpStatusCode.CREATED,
                Status = true,
                Message = "Registro creado correctamente",
                Data = _mapper.Map<CategoryPeripheralActionResponseDto>(categoryEntity)
            };
        }
        public async Task<ResponseDto<CategoryPeripheralActionResponseDto>> EditAsync(CategoryPeripheralEditDto dto, Guid id)
        {
            var categoryEntity = await _context.CategoriesPeripherals.FirstOrDefaultAsync(x => x.Id == id);

            if (categoryEntity is null)
            {
                return new ResponseDto<CategoryPeripheralActionResponseDto>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Status = false,
                    Message = "Registro no encontrado",
                };
            }

            _mapper.Map<CategoryPeripheralEditDto, CategoryPeripheralEntity>(dto, categoryEntity);

            _context.CategoriesPeripherals.Update(categoryEntity);
            await _context.SaveChangesAsync();

            return new ResponseDto<CategoryPeripheralActionResponseDto>
            {
                StatusCode = HttpStatusCode.Ok,
                Status = true,
                Message = "Registro editado correctamente",
                Data = _mapper.Map<CategoryPeripheralActionResponseDto>(categoryEntity)
            };
        }
        public async Task<ResponseDto<CategoryPeripheralActionResponseDto>> DeleteAsync(Guid id)
        {
            var categoryEntity = await _context.CategoriesPeripherals.FirstOrDefaultAsync(x => x.Id == id);

            if (categoryEntity is null)
            {
                return new ResponseDto<CategoryPeripheralActionResponseDto>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Status = false,
                    Message = "Registro no encontrado",
                };
            }

            _context.CategoriesPeripherals.Remove(categoryEntity);
            await _context.SaveChangesAsync();

            return new ResponseDto<CategoryPeripheralActionResponseDto>
            {
                StatusCode = HttpStatusCode.Ok,
                Status = true,
                Message = "Registro eliminado Correctamente",
                Data = _mapper.Map<CategoryPeripheralActionResponseDto>(categoryEntity)
            };
        }
    }
}