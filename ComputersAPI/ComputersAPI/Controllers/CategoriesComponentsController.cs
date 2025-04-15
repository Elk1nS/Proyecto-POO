using ComputersAPI.Dtos.CategoriesComponents;
using ComputersAPI.Dtos.Common;
using ComputersAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ComputersAPI.Controllers
{
    [ApiController]
    [Route("api/categories_components")]
    public class CategoriesComponentsController : ControllerBase
    {
        private readonly ICategoriesComponentsService _categoriesService;

        public CategoriesComponentsController(ICategoriesComponentsService categoriesService)
        {
            _categoriesService = categoriesService;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseDto<List<CategoryComponentDto>>>> GetList()
        {
            var response = await _categoriesService.GetListAsync();

            return StatusCode(response.StatusCode, new
            {
                response.Status,
                response.Message,
                response.Data

            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDto<CategoryComponentDto>>> GetOne(Guid id)
        {
            var response = await _categoriesService.GetOneByIdAsync(id);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<ActionResult<ResponseDto<CategoryComponentActionResponseDto>>> Post([FromBody] CategoryComponentCreateDto dto)
        {
            var response = await _categoriesService.CreateAsync(dto);

            return StatusCode(response.StatusCode, new
            {
                response.Status,
                response.Message,
                response.Data,
            });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseDto<CategoryComponentActionResponseDto>>> Edit([FromBody] CategoryComponentEditDto dto, Guid Id)
        {
            var response = await _categoriesService.EditAsync(dto, Id);

            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDto<CategoryComponentActionResponseDto>>> Delete(Guid id)
        {
            var response = await _categoriesService.DeleteAsync(id);

            return StatusCode(response.StatusCode, response);
        }
    }
}