using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend_project_sample.Models
{
    [Microsoft.EntityFrameworkCore.Index(nameof(product_name), IsUnique = true)]
    public class Product
    {
        [Key]
        public Guid id { get; set; }
        required public string product_name { get; set; }
        public string? description { get; set; }
        public decimal? quantity { get; set; } //default quantity per product
        required public decimal price { get; set; } //price per unit
        public DateTime? last_updated_time { get; set; }
    }
    public class NewProduct
    {
        required public string product_name { get; set; }
        public string? description { get; set; }
        public decimal? quantity { get; set; }
        required public decimal price { get; set; }
    }
    public class UpdateProduct
    {
        required public string product_name { get; set; }
        public string? description { get; set; }
        public decimal? quantity { get; set; }
        required public decimal price { get; set; }
    }
    public class ProductDTO
    {
        public Guid id { get; set; }
        required public string product_name { get; set; }
        public string? description { get; set; }
        public decimal? quantity { get; set; }
        required public decimal price { get; set; }
        public DateTime? last_updated_time { get; set; }
    }
}
