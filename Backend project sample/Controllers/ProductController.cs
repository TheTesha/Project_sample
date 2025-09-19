using Microsoft.AspNetCore.Mvc;
using Backend_project_sample.Models;
using Backend_project_sample.Services.Products;

namespace Backend_project_sample.Controllers.Products
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private ProductService _productService;
        public ProductController(ProductService productService)
        {
            _productService = productService;
        }
        [HttpPost]
        public async Task<ActionResult<ProductDTO>> GetAllProductsAsync(int page, int perpage)
        {
            return Ok(await _productService.GetAllProductsAsync(page, perpage));
        }
        [Route("new")]
        [HttpPost]
        public async Task<ActionResult<ProductDTO>> AddProduct([FromBody] NewProduct newProduct)
        {
            return Ok(await _productService.CreateProductAsync(newProduct));
        }
        [HttpDelete]
        public async Task<ActionResult<ProductDTO?>> DeleteProductAsync(Guid id)
        {
            var product = await _productService.DeleteProductAsync(id);
            if (product == null)
                return NotFound();
            return Ok(product);
        }
        [HttpPut]
        public async Task<ActionResult<Product?>> UpdateProductAsync(Guid id, [FromBody] UpdateProduct newProduct)
        {
            var product = await _productService.UpdateProductAsync(id, newProduct);
            if (product == null)
                return NotFound();
            return Ok(product);
        }
    }
}
