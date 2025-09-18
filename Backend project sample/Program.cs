using Microsoft.EntityFrameworkCore;
using Backend_project_sample.Repositories.Products;
using Backend_project_sample.Services.Products;

var builder = WebApplication.CreateBuilder(args);

// DB Setup
builder.Services.AddDbContext<Backend_project_sample.Data.ApplicationDBContext>(options =>
    options.UseNpgsql(
        connectionString: builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

// Add services to the container.
builder.Services.AddControllersWithViews();

//mapings
builder.Services.AddScoped<ProductRepository>();
builder.Services.AddScoped<ProductService>();


// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
