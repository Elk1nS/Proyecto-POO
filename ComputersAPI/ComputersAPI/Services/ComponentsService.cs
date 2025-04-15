using AutoMapper;
using ComputersAPI.Constants;
using ComputersAPI.Database;
using ComputersAPI.Database.Entities;
using ComputersAPI.Dtos.Common;
using ComputersAPI.Dtos.Components;
using ComputersAPI.Dtos.Computers;
using ComputersAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ComputersAPI.Services
{
    public class ComponentsService : IComponentsService
    {
        private readonly ComputersDbContext _context;
        private readonly IMapper _mapper;

        public ComponentsService(ComputersDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResponseDto<List<ComponentDto>>> GetListAsync()
        {
            var componetsEntity = await _context.Components.Include(c => c.CategoryComponent).ToListAsync();

            var componentsDto = _mapper.Map<List<ComponentDto>>(componetsEntity);

            return new ResponseDto<List<ComponentDto>>
            {
                StatusCode = HttpStatusCode.Ok,
                Status = true,
                Message = componetsEntity.Count() > 0 ? "Registros encontrados" : "No se encontraron registros",
                Data = componentsDto
            };
        }

        public async Task<ResponseDto<ComponentDto>> GetOneByIdAsync(Guid id)
        {
            var componentEntity = await _context.Components.Include(x => x.CategoryComponent).FirstOrDefaultAsync(x => x.Id == id);

            if (componentEntity is null)
            {
                return new ResponseDto<ComponentDto>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Status = false,
                    Message = "Registro no encontrado"
                };
            }

            return new ResponseDto<ComponentDto>
            {
                StatusCode = HttpStatusCode.Ok,
                Status = true,
                Message = "Registro encontrado",
                Data = _mapper.Map<ComponentDto>(componentEntity)
            };
        }

        public async Task<ResponseDto<ComponentActionResponseDto>> CreateAsync(ComponentCreateDto dto)
        {

            var componentEntity = _mapper.Map<ComponentEntity>(dto);

            _context.Components.Add(componentEntity);
            await _context.SaveChangesAsync();

            return new ResponseDto<ComponentActionResponseDto>
            {
                StatusCode = HttpStatusCode.CREATED,
                Status = true,
                Message = "Registro creado correctamente",
                Data = _mapper.Map<ComponentActionResponseDto>(componentEntity)
            };
        }

        public async Task<ResponseDto<ComponentActionResponseDto>> EditAsync(ComponentEditDto dto, Guid id)
        {
            var componentEntity = await _context.Components.FirstOrDefaultAsync(x => x.Id == id);

            if (componentEntity is null)
            {
                return new ResponseDto<ComponentActionResponseDto>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Status = false,
                    Message = "Registro no encontrado",
                };
            }

            _mapper.Map<ComponentEditDto, ComponentEntity>(dto, componentEntity);

            _context.Components.Update(componentEntity);
            await _context.SaveChangesAsync();

            return new ResponseDto<ComponentActionResponseDto>
            {
                StatusCode = HttpStatusCode.Ok,
                Status = true,
                Message = "Registro editado correctamente",
                Data = _mapper.Map<ComponentActionResponseDto>(componentEntity)
            };
        }

        public async Task<ResponseDto<ComponentActionResponseDto>> DeleteAsync(Guid id)
        {
            var componentEntity = await _context.Components.FirstOrDefaultAsync(x => x.Id == id);

            if (componentEntity is null)
            {
                return new ResponseDto<ComponentActionResponseDto>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Status = false,
                    Message = "Registro no encontrado",
                };
            }

            _context.Components.Remove(componentEntity);
            await _context.SaveChangesAsync();

            return new ResponseDto<ComponentActionResponseDto>
            {
                StatusCode = HttpStatusCode.Ok,
                Status = true,
                Message = "Registro eliminado Correctamente",
                Data = _mapper.Map<ComponentActionResponseDto>(componentEntity)
            };
        }
    }
}
