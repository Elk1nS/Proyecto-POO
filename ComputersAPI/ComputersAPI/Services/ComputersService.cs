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
            // Cargamos las computadoras con sus relaciones de componentes y periféricos usando Include
            var computersEntity = await _context.Computers
                .Include(c => c.ComputerComponents)       // Incluimos los componentes relacionados
                .ThenInclude(cc => cc.Component)
                .ThenInclude(c => c.CategoryComponent) // se incluye la categoría del componente
                .Include(c => c.ComputerPeripherals)     // Incluimos los periféricos relacionados
                .ThenInclude(cp => cp.Peripheral)
                .ThenInclude(c => c.CategoryPeripheral)
                .ToListAsync();

            // Mapear las entidades a los DTOs
            var computersDto = _mapper.Map<List<ComputerDto>>(computersEntity);

            foreach (var computerDto in computersDto)
            {
                var totalComponents = computerDto.Components?.Sum(c => c.Price) ?? 0;
                var totalPeripherals = computerDto.Peripherals?.Sum(p => p.Price) ?? 0;
                computerDto.TotalPrice = totalComponents + totalPeripherals;
            }

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
            var computerEntity = await _context.Computers
                .Include(c => c.ComputerComponents)
                    .ThenInclude(cc => cc.Component)
                    .ThenInclude(c => c.CategoryComponent)
                .Include(c => c.ComputerPeripherals)
                    .ThenInclude(cp => cp.Peripheral)
                    .ThenInclude(c => c.CategoryPeripheral)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (computerEntity is null)
            {
                return new ResponseDto<ComputerDto>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Status = false,
                    Message = "Registro no encontrado"
                };
            }

            // Mapear la entidad a DTO
            var computerDto = _mapper.Map<ComputerDto>(computerEntity);

            // Calcular TotalPrice
            var totalComponents = computerDto.Components?.Sum(c => c.Price) ?? 0;
            var totalPeripherals = computerDto.Peripherals?.Sum(p => p.Price) ?? 0;
            computerDto.TotalPrice = totalComponents + totalPeripherals;

            return new ResponseDto<ComputerDto>
            {
                StatusCode = HttpStatusCode.Ok,
                Status = true,
                Message = "Registro encontrado",
                Data = computerDto 
            };
        }

     
        public async Task<ResponseDto<ComputerActionResponseDto>> CreateAsync(ComputerCreateDto dto)
        {
            if ((dto.ComponentsIds == null || !dto.ComponentsIds.Any()) &&
    (dto.PeripheralsIds == null || !dto.PeripheralsIds.Any()))
            {
                return new ResponseDto<ComputerActionResponseDto>
                {
                    StatusCode = HttpStatusCode.BAD_REQUEST,
                    Status = false,
                    Message = "Debe agregar al menos un componente o un periférico a la computadora."
                };
            }

            if (dto.ComponentsIds != null && dto.ComponentsIds.Count != dto.ComponentsIds.Distinct().Count())
            {
                return new ResponseDto<ComputerActionResponseDto>
                {
                    StatusCode = HttpStatusCode.BAD_REQUEST,
                    Status = false,
                    Message = "No se puede asignar el mismo componente más de una vez."
                };
            }

            if (dto.PeripheralsIds != null && dto.PeripheralsIds.Count != dto.PeripheralsIds.Distinct().Count())
            {
                return new ResponseDto<ComputerActionResponseDto>
                {
                    StatusCode = HttpStatusCode.BAD_REQUEST,
                    Status = false,
                    Message = "No se puede asignar el mismo periférico más de una vez."
                };
            }


            var componentIds = dto.ComponentsIds ?? new List<Guid>();
            var peripheralIds = dto.PeripheralsIds ?? new List<Guid>();

            // Validar existencia de componentes
            var validComponentIds = await _context.Components
                .Where(c => componentIds.Contains(c.Id))
                .Select(c => c.Id)
                .ToListAsync();

            var missingComponentIds = componentIds.Except(validComponentIds).ToList();
            if (missingComponentIds.Any())
            {
                return new ResponseDto<ComputerActionResponseDto>
                {
                    StatusCode = HttpStatusCode.BAD_REQUEST,
                    Status = false,
                    Message = "Uno o más componentes no existen en la base de datos."
                };
            }

            // Validar existencia de periféricos
            var validPeripheralIds = await _context.Peripherals
                .Where(p => peripheralIds.Contains(p.Id))
                .Select(p => p.Id)
                .ToListAsync();

            var missingPeripheralIds = peripheralIds.Except(validPeripheralIds).ToList();
            if (missingPeripheralIds.Any())
            {
                return new ResponseDto<ComputerActionResponseDto>
                {
                    StatusCode = HttpStatusCode.BAD_REQUEST,
                    Status = false,
                    Message = "Uno o más periféricos no existen en la base de datos."
                };
            }

            var computerEntity = _mapper.Map<ComputerEntity>(dto);
            computerEntity.Id = Guid.NewGuid();
            computerEntity.CreatedAt = DateTime.Now; // Fecha de creación

            //  Calcular y asignar el total ANTES de guardar
            var selectedComponents = await _context.Components
                .Where(c => dto.ComponentsIds.Contains(c.Id))
                .ToListAsync();

            var selectedPeripherals = await _context.Peripherals
                .Where(p => dto.PeripheralsIds.Contains(p.Id))
                .ToListAsync();

            computerEntity.TotalPrice = selectedComponents.Sum(c => c.Price) + selectedPeripherals.Sum(p => p.Price);

            // Agregamos la computadora
            _context.Computers.Add(computerEntity);

            // Relación con Componentes
            if (dto.ComponentsIds != null && dto.ComponentsIds.Any())
            {
                foreach (var componentId in dto.ComponentsIds)
                {
                    _context.ComputerComponents.Add(new ComputerComponentEntity
                    {
                        Id = Guid.NewGuid(),
                        ComputerId = computerEntity.Id,
                        ComponentId = componentId
                    });
                }
            }

            // Relación con Periféricos
            if (dto.PeripheralsIds != null && dto.PeripheralsIds.Any())
            {
                foreach (var peripheralId in dto.PeripheralsIds)
                {
                    _context.ComputerPeripherals.Add(new ComputerPeripheralEntity
                    {
                        Id = Guid.NewGuid(),
                        ComputerId = computerEntity.Id,
                        PeripheralId = peripheralId
                    });
                }
            }

            await _context.SaveChangesAsync();

            var fullComputer = await _context.Computers
     .Include(c => c.ComputerComponents)
         .ThenInclude(cc => cc.Component)
             .ThenInclude(c => c.CategoryComponent)
     .Include(c => c.ComputerPeripherals)
         .ThenInclude(cp => cp.Peripheral)
             .ThenInclude(p => p.CategoryPeripheral)
     .FirstOrDefaultAsync(c => c.Id == computerEntity.Id);

            //// Volvemos a consultar la computadora con sus relaciones
            //var createdComputer = await _context.Computers
            //    .Include(c => c.ComputerComponents)
            //        .ThenInclude(cc => cc.Component)
            //        .ThenInclude(c => c.CategoryComponent)
            //    .Include(c => c.ComputerPeripherals)
            //        .ThenInclude(cp => cp.Peripheral)
            //        .ThenInclude(p => p.CategoryPeripheral)
            //    .FirstOrDefaultAsync(x => x.Id == computerEntity.Id);

            //var computerDto = _mapper.Map<ComputerDto>(createdComputer);

            //// Calcular total
            //var totalComponents = computerDto.Components?.Sum(c => c.Price) ?? 0;
            //var totalPeripherals = computerDto.Peripherals?.Sum(p => p.Price) ?? 0;
            //computerDto.TotalPrice = totalComponents + totalPeripherals;

            return new ResponseDto<ComputerActionResponseDto>
            {
                StatusCode = HttpStatusCode.CREATED,
                Status = true,
                Message = "Registro creado correctamente",
                Data = _mapper.Map<ComputerActionResponseDto>(fullComputer)
            };
        }

        public async Task<ResponseDto<ComputerActionResponseDto>> EditAsync(ComputerEditDto dto, Guid id)
        {
            var computerEntity = await _context.Computers
                .FirstOrDefaultAsync(x => x.Id == id);

            if (computerEntity is null)
            {
                return new ResponseDto<ComputerActionResponseDto>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Status = false,
                    Message = "Registro no encontrado",
                };
            }

            // Mapear nuevos datos básicos
            _mapper.Map(dto, computerEntity);

            // Cargar relaciones actuales
            var existingComponents = await _context.ComputerComponents
                .Where(cc => cc.ComputerId == id)
                .ToListAsync();

            var existingPeripherals = await _context.ComputerPeripherals
                .Where(cp => cp.ComputerId == id)
                .ToListAsync();

            // Eliminar relaciones viejas
            _context.ComputerComponents.RemoveRange(existingComponents);
            _context.ComputerPeripherals.RemoveRange(existingPeripherals);

            // Agregar nuevas relaciones
            foreach (var componentId in dto.ComponentsIds)
            {
                _context.ComputerComponents.Add(new ComputerComponentEntity
                {
                    Id = Guid.NewGuid(),
                    ComputerId = computerEntity.Id,
                    ComponentId = componentId
                });
            }

            foreach (var peripheralId in dto.PeripheralsIds)
            {
                _context.ComputerPeripherals.Add(new ComputerPeripheralEntity
                {
                    Id = Guid.NewGuid(),
                    ComputerId = computerEntity.Id,
                    PeripheralId = peripheralId
                });
            }

            // Obtener precios de los componentes
            var componentPrices = await _context.Components
                .Where(c => dto.ComponentsIds.Contains(c.Id))
                .Select(c => c.Price)
                .ToListAsync();

            // Obtener precios de los periféricos
            var peripheralPrices = await _context.Peripherals
                .Where(p => dto.PeripheralsIds.Contains(p.Id))
                .Select(p => p.Price)
                .ToListAsync();

            // Calcular el precio total
            computerEntity.TotalPrice = componentPrices.Sum() + peripheralPrices.Sum();


            // Guardar cambios
            await _context.SaveChangesAsync();

            computerEntity.ComputerComponents = await _context.ComputerComponents
    .Where(cc => cc.ComputerId == computerEntity.Id)
    .Include(cc => cc.Component)
        .ThenInclude(c => c.CategoryComponent)
    .ToListAsync();

            computerEntity.ComputerPeripherals = await _context.ComputerPeripherals
                .Where(cp => cp.ComputerId == computerEntity.Id)
                .Include(cp => cp.Peripheral)
                    .ThenInclude(p => p.CategoryPeripheral)
                .ToListAsync();


            return new ResponseDto<ComputerActionResponseDto>
            {
                StatusCode = HttpStatusCode.Ok,
                Status = true,
                Message = "Registro editado correctamente",
                Data = _mapper.Map<ComputerActionResponseDto>(computerEntity)
            };
        }


        //public async Task<ResponseDto<ComputerActionResponseDto>> EditAsync(ComputerEditDto dto, Guid id)
        //{
        //    var computerEntity = await _context.Computers
        //        .Include(c => c.ComputerComponents) //  Incluye relaciones existentes
        //        .Include(c => c.ComputerPeripherals) //  Incluye relaciones existentes
        //        .FirstOrDefaultAsync(x => x.Id == id);

        //    if (computerEntity is null)
        //    {
        //        return new ResponseDto<ComputerActionResponseDto>
        //        {
        //            StatusCode = HttpStatusCode.NOT_FOUND,
        //            Status = false,
        //            Message = "Registro no encontrado",
        //        };
        //    }

        //    //  Actualiza los datos básicos de la computadora
        //    _mapper.Map<ComputerEditDto, ComputerEntity>(dto, computerEntity);

        //    //  Elimina relaciones actuales
        //    _context.ComputerComponents.RemoveRange(computerEntity.ComputerComponents);
        //    _context.ComputerPeripherals.RemoveRange(computerEntity.ComputerPeripherals);

        //    //  Agrega nuevas relaciones con Componentes
        //    computerEntity.ComputerComponents = dto.ComponentsIds.Select(id => new ComputerComponentEntity
        //    {
        //        Id = Guid.NewGuid(),
        //        ComputerId = computerEntity.Id,
        //        ComponentId = id
        //    }).ToList();

        //    //  Agrega nuevas relaciones con Periféricos
        //    computerEntity.ComputerPeripherals = dto.PeripheralsIds.Select(id => new ComputerPeripheralEntity
        //    {
        //        Id = Guid.NewGuid(),
        //        ComputerId = computerEntity.Id,
        //        PeripheralId = id
        //    }).ToList();

        //    _context.Computers.Update(computerEntity);
        //    await _context.SaveChangesAsync();

        //    return new ResponseDto<ComputerActionResponseDto>
        //    {
        //        StatusCode = HttpStatusCode.Ok,
        //        Status = true,
        //        Message = "Registro editado correctamente",
        //        Data = _mapper.Map<ComputerActionResponseDto>(computerEntity)
        //    };
        //}



        public async Task<ResponseDto<ComputerActionResponseDto>> DeleteAsync(Guid id)
        {
            var computerEntity = await _context.Computers
    .Include(c => c.ComputerComponents)
        .ThenInclude(cc => cc.Component)
            .ThenInclude(c => c.CategoryComponent)
    .Include(c => c.ComputerPeripherals)
        .ThenInclude(cp => cp.Peripheral)
            .ThenInclude(p => p.CategoryPeripheral)
    .FirstOrDefaultAsync(x => x.Id == id);

            if (computerEntity is null)
            {
                return new ResponseDto<ComputerActionResponseDto>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Status = false,
                    Message = "Registro no encontrado",
                };
            }

            var mappedData = _mapper.Map<ComputerActionResponseDto>(computerEntity);


            // Eliminar relaciones
            _context.ComputerComponents.RemoveRange(computerEntity.ComputerComponents);
            _context.ComputerPeripherals.RemoveRange(computerEntity.ComputerPeripherals);

            // Eliminar la computadora
            _context.Computers.Remove(computerEntity);
            await _context.SaveChangesAsync();

            return new ResponseDto<ComputerActionResponseDto>
            {
                StatusCode = HttpStatusCode.Ok,
                Status = true,
                Message = "Registro eliminado correctamente",
                Data = mappedData
            };
        }

        
    }
}
