using ComputersAPI.Dtos.Common;
using ComputersAPI.Dtos.Components;
using ComputersAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ComputersAPI.Controllers
{
    [ApiController]
    [Route("api/components")]

    public class ComponentsController : ControllerBase
    {
        private readonly IComponentsService _componentsService;

        public ComponentsController(IComponentsService componentsService)
        {
            _componentsService = componentsService;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseDto<List<ComponentDto>>>> GetList()
        {
            var response = await _componentsService.GetListAsync();

            return StatusCode(response.StatusCode, new
            {
                response.Status,
                response.Message,
                response.Data

            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDto<ComponentDto>>> GetOne(Guid id)
        {
            var response = await _componentsService.GetOneByIdAsync(id);

            return StatusCode(response.StatusCode, new
            {
                response.Status,
                response.Message,
                response.Data
            });
        }

        [HttpPost]
        public async Task<ActionResult<ResponseDto<ComponentActionResponseDto>>> Post([FromBody] ComponentCreateDto dto)
        {
            var response = await _componentsService.CreateAsync(dto);

            return StatusCode(response.StatusCode, new
            {
                response.Status,
                response.Message,
                response.Data,
            });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseDto<ComponentActionResponseDto>>> Edit([FromBody] ComponentEditDto dto, Guid Id)
        {
            var response = await _componentsService.EditAsync(dto, Id);

            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDto<ComponentActionResponseDto>>> Delete(Guid id)
        {
            var response = await _componentsService.DeleteAsync(id);

            return StatusCode(response.StatusCode, response);
        }
    }
}