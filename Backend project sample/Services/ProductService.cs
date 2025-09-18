using Microsoft.AspNetCore.Mvc;
using Backend_project_sample.Models;
using Backend_project_sample.Repositories.Products;

namespace Backend_project_sample.Services.Products
{
    public class ProductService
    {
        private ProductRepository _productRepository;
        public ProductService(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<List<ProductDTO>> GetAllProductsAsync(int page, int perpage)
        {
            return await _productRepository.GetAllProductsAsync(page, perpage);
        }
        public async Task<Product> CreateProductAsync(NewProduct product)
        {
            Guid id = Guid.NewGuid();
            Product newProduct = new Product()
            {
                product_name = product.product_name,
                description = product.description,
                quantity = product.quantity,
                price = product.price
            };
            newProduct.id = id;
            newProduct.last_updated_time = DateTime.UtcNow;

            return await _productRepository.AddAsync(newProduct);
        }
        public async Task<Product?> DeleteProductAsync(Guid id)
        {
            return await _productRepository.DeleteAsync(id);
        }
        public async Task<UpdateProduct?> UpdateProductAsync(Guid id, UpdateProduct product)
        {
            return await _productRepository.UpdateAsync(id, product);
        }
    }
}
