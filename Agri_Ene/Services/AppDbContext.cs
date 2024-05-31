using Agri_Ene.Data;
using Agri_Ene.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Reflection.Emit;

namespace Agri_Ene.Services
{
    /* AppDbContext class  that inherits from IdentityDbContext<AgriUser>,
     * provides access to identity features.*/
    public class AppDbContext : IdentityDbContext<AgriUser>
    {
        // Constructor to inject configurations into the database 
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
           

        }
        //----------------------------------------------------------------------------//
        // This method is called to build the model that maps to the database schema
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Call the base method to ensure the base configuration is applied
            base.OnModelCreating(modelBuilder);

            // Define custom configurations for the identity roles (employee and farmer)
            var employee = new IdentityRole("employee");
            employee.NormalizedName = "employee";
            var farmer = new IdentityRole("farmer");
            farmer.NormalizedName = "farmer";

            // Seed the database with predefined identity roles
            modelBuilder.Entity<IdentityRole>().HasData(employee, farmer);
        }
        //----------------------------------------------------------------------------//

        // DbSet for the Product entity to interact with the database table
        public DbSet<Product> Products { get; set; }
    }
}
//__---____---____---____---____---____---____---__.ooo END OF FILE ooo.__---____---____---____---____---____---____---__\\
