using Agri_Ene.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Agri_Ene.Models;
using Agri_Ene.Interface;
using Agri_Ene.Repository;
using System.Security.Claims;

// Create a new instance of the web application builder
var builder = WebApplication.CreateBuilder(args);

// Add controllers and views services to the container
builder.Services.AddControllersWithViews();

// Set the data directory to the directory of the application's executable file
AppDomain.CurrentDomain.SetData("DataDirectory", AppDomain.CurrentDomain.BaseDirectory);

// Configure the database connection
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Sauce"));

});

// Add default identity services for AgriUser with additional options
builder.Services.AddDefaultIdentity<AgriUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>() // Add role services
    .AddEntityFrameworkStores<AppDbContext>(); // Use the AppDbContext for identity data storage

// Enable runtime compilation of Razor views for development
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

// Register the IProductRepository and ProductRepository services for dependency injection
builder.Services.AddScoped<IProductRepository, ProductRepository>();

// Register the ClaimsPrincipal service
builder.Services.AddScoped<ClaimsPrincipal>(provider =>
    provider.GetService<IHttpContextAccessor>()?.HttpContext?.User);

// Build the application
var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    // Handle errors in production environment
    app.UseExceptionHandler("/Home/Error");

    // Enable HTTP Strict Transport Security (HSTS)
    // You may want to change the duration for production scenarios
    app.UseHsts();
}

// Redirect HTTP requests to HTTPS
app.UseHttpsRedirection();

// Serve static files like images, CSS, and JavaScript
app.UseStaticFiles();

// Enable routing
app.UseRouting();

// Enable authentication and authorization
app.UseAuthorization();



// Define endpoint routes
app.UseEndpoints(endpoints =>
{
    // Map default controller route
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    // Map Razor pages
    endpoints.MapRazorPages();
});

// Run the application
app.Run();
//__---____---____---____---____---____---____---__.ooo END OF FILE ooo.__---____---____---____---____---____---____---__\\