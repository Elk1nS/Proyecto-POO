using ComputersAPI.Dtos.Common;
using ComputersAPI.Dtos.Peripherals;
using ComputersAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ComputersAPI.Controllers
{
    [ApiController]
    [Route("api/peripherals")]
    public class PeripheralsController : ControllerBase
    {
        private readonly IPeripheralsService _peripheralsService;

        public PeripheralsController(IPeripheralsService peripheralsService)
        {
            _peripheralsService = peripheralsService;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseDto<List<PeripheralDto>>>> GetList()
        {
            var response = await _peripheralsService.GetListAsync();

            return StatusCode(response.StatusCode, new
            {
                response.Status,
                response.Message,
                response.Data

            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDto<PeripheralDto>>> GetOne(Guid id)
        {
            var response = await _peripheralsService.GetOneByIdAsync(id);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<ActionResult<ResponseDto<PeripheralActionResponseDto>>> Post([FromBody] PeripheralCreateDto dto)
        {
            var response = await _peripheralsService.CreateAsync(dto);

            return StatusCode(response.StatusCode, new
            {
                response.Status,
                response.Message,
                response.Data,
            });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseDto<PeripheralActionResponseDto>>> Edit([FromBody] PeripheralEditDto dto, Guid Id)
        {
            var response = await _peripheralsService.EditAsync(dto, Id);

            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDto<PeripheralActionResponseDto>>> Delete(Guid id)
        {
            var response = await _peripheralsService.DeleteAsync(id);

            return StatusCode(response.StatusCode, response);
        }
    }
}