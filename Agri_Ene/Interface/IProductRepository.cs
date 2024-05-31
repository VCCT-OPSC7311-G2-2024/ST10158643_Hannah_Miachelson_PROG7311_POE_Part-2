using Agri_Ene.Data;
using Agri_Ene.Models;

namespace Agri_Ene.Interface
{
    // Interface for interacting for product-related data
    public interface IProductRepository
    {   
        /* Get Commands*/
        // Retrieve all products
        Task<IEnumerable<Product>> GetAll();

        // Retrieve all products for employees
        Task<IEnumerable<Product>> GetAllEmp();

        // Retrieve a product by its ID asynchronously
        Task<Product> GetByIdAsync(int id);

        // Retrieve products associated with a specific farmer
        Task<IEnumerable<Product>> GetByFarmer(string farmer);

        // Retrieve the name of the farmer associated with a product
        Task<(string firstName, string lastName)> GetFarmer(int? productId);

        // Retrieve details of the farmer associated with a product
        Task<AgriUser> GetFarmerDetails();

        // Retrieve products within a specified date range
        Task<IEnumerable<Product>> GetProductsByDateRange(DateTime startDate, DateTime endDate);

        // Retrieve products by category
        Task<IEnumerable<Product>> GetProdBy_Category(ProductCategories? category);

        // Retrieve the name of a product category
        string GetCategoryName(ProductCategories category);

        //----------------------------------------------------------------------------//

        /* CRUD commands*/

        // Add a new product
        bool Add(Product product);

        // Delete a product
        bool Delete(Product product);

        // Save changes to the data store
        bool Save();
        //----------------------------------------------------------------------------//

    }
}//__---____---____---____---____---____---____---__.ooo END OF FILE ooo.__---____---____---____---____---____---____---__\\
