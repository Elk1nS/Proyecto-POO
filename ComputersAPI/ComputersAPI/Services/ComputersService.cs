using AutoMapper;
using ComputersAPI.Constants;
using ComputersAPI.Database;
using ComputersAPI.Database.Entities;
using ComputersAPI.Dtos.Common;
using ComputersAPI.Dtos.Computers;
using ComputersAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ComputersAPI.Services
{
    public class ComputersService : IComputersService
    {
        private readonly ComputersDbContext _context;
        private readonly IMapper _mapper;

        public ComputersService(ComputersDbContext context, IMapper mapper) 
        { 
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResponseDto<List<ComputerDto>>> GetListAsync()
        {
            var computersEntity = await _context.Computers.ToListAsync();

            var computersDto = _mapper.Map<List<ComputerDto>>(computersEntity);

            return new ResponseDto<List<ComputerDto>>
            {
                StatusCode = HttpStatusCode.Ok,
                Status = true,
                Message = computersEntity.Count() > 0 ? "Registros encontrados" : "No se encontraron registros",
                Data = computersDto
            };
        }

        public async Task<ResponseDto<ComputerDto>> GetOneByIdAsync(Guid id)
        {
            var computerEntity = await _context.Computers.FirstOrDefaultAsync(x => x.Id == id);

            if (computerEntity is null)
            {
                return new ResponseDto<ComputerDto>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Status = false,
                    Message = "Registro no encontrado"
                };
            }

            return new ResponseDto<ComputerDto>
            {
                StatusCode = HttpStatusCode.Ok,
                Status = true,
                Message = "Registro encontrado",
                Data = _mapper.Map<ComputerDto>(computerEntity)
            };
        }

        public async Task<ResponseDto<ComputerActionResponseDto>> CreateAsync(ComputerCreateDto dto)
        {

            var computerEntity = _mapper.Map<ComputerEntity>(dto);

            _context.Computers.Add(computerEntity);
            await _context.SaveChangesAsync();

            return new ResponseDto<ComputerActionResponseDto>
            {
                StatusCode = HttpStatusCode.CREATED,
                Status = true,
                Message = "Registro creado correctamente",
                Data = _mapper.Map<ComputerActionResponseDto>(computerEntity)
            };
        }

        public async Task<ResponseDto<ComputerActionResponseDto>> EditAsync(ComputerEditDto dto, Guid id)
        {
            var computerEntity = await _context.Computers.FirstOrDefaultAsync(x => x.Id == id);

            if (computerEntity is null)
            {
                return new ResponseDto<ComputerActionResponseDto>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Status = false,
                    Message = "Registro no encontrado",
                };
            }

            _mapper.Map<ComputerEditDto, ComputerEntity>(dto, computerEntity);

            _context.Computers.Update(computerEntity);
            await _context.SaveChangesAsync();

            return new ResponseDto<ComputerActionResponseDto>
            {
                StatusCode = HttpStatusCode.Ok,
                Status = true,
                Message = "Registro editado correctamente",
                Data = _mapper.Map<ComputerActionResponseDto>(computerEntity)
            };
        }

        public async Task<ResponseDto<ComputerActionResponseDto>> DeleteAsync(Guid id)
        {
            var computerEntity = await _context.Computers.FirstOrDefaultAsync(x => x.Id == id);

            if (computerEntity is null)
            {
                return new ResponseDto<ComputerActionResponseDto>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Status = false,
                    Message = "Registro no encontrado",
                };
            }

            _context.Computers.Remove(computerEntity);
            await _context.SaveChangesAsync();

            return new ResponseDto<ComputerActionResponseDto>
            {
                StatusCode = HttpStatusCode.Ok,
                Status = true,
                Message = "Registro eliminado Correctamente",
                Data = _mapper.Map<ComputerActionResponseDto>(computerEntity)
            };
        }
    }
}
