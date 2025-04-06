using Microsoft.AspNetCore.Mvc;
using ComputersAPI.Database.Entities;
using ComputersAPI.Dtos.Common;
using ComputersAPI.Dtos.Computers;
using ComputersAPI.Services.Interfaces;

namespace ComputersAPI.Controllers
{

    [ApiController]
    [Route("api/computers")]
    public class ComputersController : ControllerBase
    {
        private readonly IComputersService _computersService;

        public ComputersController(IComputersService computersService) 
        { 
            _computersService = computersService;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseDto<List<ComputerDto>>>> GetList()
        {
            var response = await _computersService.GetListAsync();

            return StatusCode(response.StatusCode, new
            {
                response.Status,
                response.Message,
                response.Data

            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDto<ComputerDto>>> GetOne(Guid id)
        {
            var response = await _computersService.GetOneByIdAsync(id);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<ActionResult<ResponseDto<ComputerActionResponseDto>>> Post([FromBody] ComputerCreateDto dto)
        {
            var response = await _computersService.CreateAsync(dto);

            return StatusCode(response.StatusCode, new
            {
                response.Status,
                response.Message,
                response.Data,
            });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseDto<ComputerActionResponseDto>>> Edit([FromBody] ComputerEditDto dto, Guid Id)
        {
            var response = await _computersService.EditAsync(dto, Id);

            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDto<ComputerActionResponseDto>>> Delete(Guid id)
        {
            var response = await _computersService.DeleteAsync(id);

            return StatusCode(response.StatusCode, response);
        }

    }
}
