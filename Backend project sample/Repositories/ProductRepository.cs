using Microsoft.EntityFrameworkCore;
using SqlKata;
using System.Runtime.CompilerServices;
using Backend_project_sample.Models;
using WebAppBackend.Repositories;
using Backend_project_sample.Data;

namespace Backend_project_sample.Repositories.Products
{
    public class ProductRepository : RepoUpClass
    {
        private readonly ApplicationDBContext _context;

        public ProductRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<ProductDTO>> GetAllProductsAsync(int page, int perpage)
        {
            var query = new Query("Products")
                .Select("Products.*", "ud.email as email", "un.unit as unit")
                .OrderByDesc("product_name");

            var queryRes = query.Clone()
                .Limit(perpage)
                .Skip(page * perpage);

            string sql = querryCompiler.Compile(queryRes).ToString();

            List<ProductDTO> products = await _context.Products
                .FromSqlRaw(sql)
                .Select(p => new ProductDTO
                {
                    id = p.id,
                    product_name = p.product_name,
                    description = p.description,
                    quantity = p.quantity,
                    price = p.price,
                    last_updated_time = p.last_updated_time
                })
                .ToListAsync();

            return products;
        }

        public async Task<Product> AddAsync(Product product)
        {
            var result = await _context.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product?> DeleteAsync(Guid id)
        {
            var query = new Query("Products").Where("id", id).AsDelete();
            string sql = querryCompiler.Compile(query).ToString();

            Product? product = await GetProductByIdAsync(id);
            if (product != null)
                await _context.Database.ExecuteSqlRawAsync(sql);

            return product;
        }

        public async Task<UpdateProduct?> UpdateAsync(Guid id, UpdateProduct product)
        {
            var query = new Query("Products").Where("id", id).AsUpdate(new
            {
                product.product_name,
                product.description,
                product.quantity,
                product.price,
                last_updated_time = DateTime.UtcNow,
            });

            string sql = querryCompiler.Compile(query).ToString();
            await _context.Database.ExecuteSqlRawAsync(sql);

            return product;
        }

        public async Task<Product?> GetProductByIdAsync(Guid? id)
        {
            if (id == null)
                return null;

            var query = new Query("Products").Where("id", id);
            string sql = querryCompiler.Compile(query).ToString();

            Product? product = await _context.Products
                .FromSqlRaw(sql)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            return product;
        }
    }
}
