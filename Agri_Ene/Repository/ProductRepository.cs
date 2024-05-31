using Agri_Ene.Data;
using Agri_Ene.Interface;
using Agri_Ene.Models;
using Agri_Ene.Services;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Agri_Ene.Repository
{
    // ProductRepository class to implements the IProductRepository interface
    public class ProductRepository : IProductRepository
    {
        // Private field to access the database context 
        private readonly AppDbContext _context;
        // Private field to access the current user's claims
        private readonly ClaimsPrincipal _currentUser;

        // Constructor to initialize the repository with the database context and the current user's claims
        public ProductRepository(AppDbContext context, ClaimsPrincipal currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }
        //----------------------------------------------------------------------------//

        // Method to add a new product to the database
        public bool Add(Product product)
        {
            // Set the FarmerId property to the current user's ID
            product.FarmerId = _currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // Add the product to the database context
            _context.Add(product);

            // Save changes to the database and return true if successful, otherwise false
            return Save();
        }
        //----------------------------------------------------------------------------//

        // Method to delete a product from the database
        public bool Delete(Product product)
        {
            _context.Remove(product);
            return Save();
        }
        //----------------------------------------------------------------------------//

        // Method to save changes to the database
        public bool Save()
        {
            // Save changes to the database and return true if successful, otherwise false
            return _context.SaveChanges() > 0;
        }
        //----------------------------------------------------------------------------//

        // Method to retrieve a product by its ID asynchronously
        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products.FirstOrDefaultAsync(i => i.prodId == id);
        }
        //----------------------------------------------------------------------------//

        // Method to retrieve all products asynchronously
        public async Task<IEnumerable<Product>> GetAllEmp()
        {
            return await _context.Products.ToListAsync();
        }
        //----------------------------------------------------------------------------//

        // Method to retrieve all products accoring to the current user asynchronously
        public async Task<IEnumerable<Product>> GetAll()
        {
            var userId = _currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return await _context.Products
                .Where(p => p.FarmerId == userId)
                .ToListAsync();
        }
        //----------------------------------------------------------------------------//

        // Method to retrieve products according to a specificed farmer firstname asynchronously
        public async Task<IEnumerable<Product>> GetByFarmer(string farmerName)
        {
            return await _context.Products
                .Include(p => p.Farmer)
                .Where(p => p.Farmer != null && p.Farmer.FirstName == farmerName)
                .ToListAsync();
        }
        //----------------------------------------------------------------------------//

        // Method to retrieve products within a specified date range asynchronously
        public async Task<IEnumerable<Product>> GetProductsByDateRange(DateTime startDate, DateTime endDate)
        {
            return await _context.Products
                .Where(p => p.productionDate >= startDate && p.productionDate <= endDate)
                .ToListAsync();
        }
        //----------------------------------------------------------------------------//

        // Method to retrieve products by category asynchronously
        public async Task<IEnumerable<Product>> GetProdBy_Category(ProductCategories? category)
        {
            var query = _context.Products.AsQueryable();
            if (category.HasValue)
            {
                query = query.Where(p => p.prodCategory == category.Value);
            }
            return await query.ToListAsync();
        }
        //----------------------------------------------------------------------------//

        // Method to retrieve farmer details of the current farmer asynchronously
        public async Task<AgriUser> GetFarmerDetails()
        {
            var userId = _currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
        }
        //----------------------------------------------------------------------------//

        // Method to retrieve the farmer's details based on the product ID asynchronously
        public async Task<(string firstName, string lastName)> GetFarmer(int? productId)
        {
            var product = await _context.Products
                .Where(p => p.prodId == productId)
                .Include(p => p.Farmer)
                .FirstOrDefaultAsync();

            if (product != null && product.Farmer != null)
            {
                return (product.Farmer.FirstName, product.Farmer.LastName);
            }
            else
            {
                return (null, null);
            }

        }
        //----------------------------------------------------------------------------//

        // Method to get the name of a product category
        public string GetCategoryName(ProductCategories category)
        {
            string name = category.ToString();
            name = name.Replace("_", " ");
            return name;
        }
        //----------------------------------------------------------------------------//
    }
}
//__---____---____---____---____---____---____---__.ooo END OF FILE ooo.__---____---____---____---____---____---____---__\\
