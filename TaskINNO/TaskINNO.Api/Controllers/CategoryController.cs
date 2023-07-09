using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskINNO.Application.Abstractions;
using TaskINNO.Application.Models;

namespace TaskINNO.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromQuery] CreateCategoryModel model)
        {
            await _categoryService.CreateAsync(model);

            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);

            return Ok(category);
        }

        [HttpGet("Page")]
        public async Task<IActionResult> GetPageSize([FromQuery] int page, int pageSize)
        {
            var category = await _categoryService.GetPageSizeAsync(page, pageSize);

            return Ok(category);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromQuery] UpdateCategoryModel model)
        {
            await _categoryService.UpdateAsync(model);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete( int id)
        {
            await _categoryService.DeleteAsync(id);

            return Ok();
        }
    }
}
