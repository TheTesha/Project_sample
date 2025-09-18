using Microsoft.EntityFrameworkCore;
using Backend_project_sample.Models;

namespace Backend_project_sample.Data
{
    public class ApplicationDBContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
    }
}
