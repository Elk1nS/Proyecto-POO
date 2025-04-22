using AutoMapper;
using ComputersAPI.Database.Entities;
using ComputersAPI.Database;
using ComputersAPI.Dtos.Common;
using ComputersAPI.Dtos.Components;
using ComputersAPI.Services.Interfaces;
using ComputersAPI.Dtos.Peripherals;
using Microsoft.EntityFrameworkCore;
using ComputersAPI.Constants;

namespace ComputersAPI.Services
{
    public class PeripheralsService : IPeripheralsService
    {
        private readonly ComputersDbContext _context;
        private readonly IMapper _mapper;

        public PeripheralsService(ComputersDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResponseDto<List<PeripheralDto>>> GetListAsync(Guid? categoryId = null)
        {
            var query = _context.Peripherals
                .Include(c => c.CategoryPeripheral)
                .AsQueryable();

            if (categoryId.HasValue)
            {
                query = query.Where(c => c.CategoryPeripheralId == categoryId.Value);
            }

            var peripheralsEntity = await query.ToListAsync();
            var peripherlasDto = _mapper.Map<List<PeripheralDto>>(peripheralsEntity);

            return new ResponseDto<List<PeripheralDto>>
            {
                StatusCode = HttpStatusCode.Ok,
                Status = true,
                Message = peripheralsEntity.Count > 0 ? "Registros encontrados" : "No se encontraron registros",
                Data = peripherlasDto
            };
        }

        //public async Task<ResponseDto<List<PeripheralDto>>> GetListAsync(Guid? categoryId = null)
        //{
        //    var peripheralsEntity = await _context.Peripherals.Include(p => p.CategoryPeripheral).ToListAsync();

        //    var peripheralsDto = _mapper.Map<List<PeripheralDto>>(peripheralsEntity);

        //    return new ResponseDto<List<PeripheralDto>>
        //    {
        //        StatusCode = HttpStatusCode.Ok,
        //        Status = true,
        //        Message = peripheralsEntity.Count() > 0 ? "Registros encontrados" : "No se encontraron registros",
        //        Data = peripheralsDto
        //    };
        //}
        public async Task<ResponseDto<PeripheralDto>> GetOneByIdAsync(Guid id)
        {
            var peripheralEntity = await _context.Peripherals.Include(x => x.CategoryPeripheral).FirstOrDefaultAsync(x => x.Id == id);

            if (peripheralEntity == null)
            {
                return new ResponseDto<PeripheralDto>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Status = false,
                    Message = "Registro no encontrado"
                };
            }

            return new ResponseDto<PeripheralDto>
            {
                StatusCode = HttpStatusCode.Ok,
                Status = true,
                Message = "Registro encontrado",
                Data = _mapper.Map<PeripheralDto>(peripheralEntity)
            };
        }
        public async Task<ResponseDto<PeripheralActionResponseDto>> CreateAsync(PeripheralCreateDto dto)
        {
            // Validar que el periferico exista
            var categoryExists = await _context.CategoriesPeripherals.AnyAsync(c => c.Id == dto.CategoryPeripheralId);
            if (!categoryExists)
            {
                return new ResponseDto<PeripheralActionResponseDto>
                {
                    StatusCode = HttpStatusCode.BAD_REQUEST,
                    Status = false,
                    Message = "El periferico no existe. Verifique el ID de categoria"
                };
            }

            var peripheralEntity = _mapper.Map<PeripheralEntity>(dto);

            _context.Peripherals.Add(peripheralEntity);
            await _context.SaveChangesAsync();

            return new ResponseDto<PeripheralActionResponseDto>
            {
                StatusCode = HttpStatusCode.CREATED,
                Status = true,
                Message = "Registro creado correctamente",
                Data = _mapper.Map<PeripheralActionResponseDto>(peripheralEntity)
            };
        }
        public async Task<ResponseDto<PeripheralActionResponseDto>> EditAsync(PeripheralEditDto dto, Guid id)
        {
            var peripheralEntity = await _context.Peripherals.FirstOrDefaultAsync(x => x.Id == id);

            if (peripheralEntity is null)
            {
                return new ResponseDto<PeripheralActionResponseDto>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Status = false,
                    Message = "Registro no encontrado",
                };
            }

            _mapper.Map<PeripheralEditDto, PeripheralEntity>(dto, peripheralEntity);

            _context.Peripherals.Update(peripheralEntity);
            await _context.SaveChangesAsync();

            return new ResponseDto<PeripheralActionResponseDto>
            {
                StatusCode = HttpStatusCode.Ok,
                Status = true,
                Message = "Registro editado correctamente",
                Data = _mapper.Map<PeripheralActionResponseDto>(peripheralEntity)
            };
        }
        public async Task<ResponseDto<PeripheralActionResponseDto>> DeleteAsync(Guid id)
        {
            var peripheralEntity = await _context.Peripherals.FirstOrDefaultAsync(x => x.Id == id);

            if (peripheralEntity is null)
            {
                return new ResponseDto<PeripheralActionResponseDto>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Status = false,
                    Message = "Registro no encontrado",
                };
            }

            var peripheralInComputer = await _context.ComputerPeripherals.CountAsync(p => p.PeripheralId == id); // ver si hay un periferico asociado a una computadora

            if (peripheralInComputer > 0)
            {
                return new ResponseDto<PeripheralActionResponseDto>
                {
                    StatusCode = HttpStatusCode.BAD_REQUEST,
                    Status = false,
                    Message = "No se puede eliminar el periferico porque esta asociado a una o mas computadoras"
                };
            }

            _context.Peripherals.Remove(peripheralEntity);
            await _context.SaveChangesAsync();

            return new ResponseDto<PeripheralActionResponseDto>
            {
                StatusCode = HttpStatusCode.Ok,
                Status = true,
                Message = "Registro eliminado Correctamente",
                Data = _mapper.Map<PeripheralActionResponseDto>(peripheralEntity)
            };
        }
    }
}
