using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskINNO.Application.Abstractions;
using TaskINNO.Application.Models;

namespace TaskINNO.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromQuery] CreateProductModel model)
        {
            await _productService.CreateAsync(model);

            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productService.GetByIdAsync(id);

            return Ok(product);
        }

        [HttpGet("Page")]
        public async Task<IActionResult> GetPageSize([FromQuery] int page, int pageSize)
        {
            var product = await _productService.GetPageSizeAsync(page, pageSize);

            return Ok(product);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromQuery] UpdateProductModel model)
        {
            await _productService.UpdateAsync(model);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _productService.DeleteAsync(id);

            return Ok();
        }
    }
}
