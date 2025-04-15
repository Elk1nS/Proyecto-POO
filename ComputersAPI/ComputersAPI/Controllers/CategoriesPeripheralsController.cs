using ComputersAPI.Dtos.CategoriesPeripherals;
using ComputersAPI.Dtos.Common;
using ComputersAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ComputersAPI.Controllers
{
    [ApiController]
    [Route("api/categories_peripherals")]
    public class CategoriesPeripheralsController : ControllerBase
    {
        private readonly ICategoriesPeripheralsService _peripheralsService;

        public CategoriesPeripheralsController(ICategoriesPeripheralsService peripheralsService)
        {
            _peripheralsService = peripheralsService;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseDto<List<CategoryPeripheralDto>>>> GetList()
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
        public async Task<ActionResult<ResponseDto<CategoryPeripheralDto>>> GetOne(Guid id)
        {
            var response = await _peripheralsService.GetOneByIdAsync(id);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<ActionResult<ResponseDto<CategoryPeripheralActionResponseDto>>> Post([FromBody] CategoryPeripheralCreateDto dto)
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
        public async Task<ActionResult<ResponseDto<CategoryPeripheralActionResponseDto>>> Edit([FromBody] CategoryPeripheralEditDto dto, Guid Id)
        {
            var response = await _peripheralsService.EditAsync(dto, Id);

            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDto<CategoryPeripheralActionResponseDto>>> Delete(Guid id)
        {
            var response = await _peripheralsService.DeleteAsync(id);

            return StatusCode(response.StatusCode, response);
        }
    }
}