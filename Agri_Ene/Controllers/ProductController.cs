using Agri_Ene.Data;
using Agri_Ene.Interface;
using Agri_Ene.Models;
using Agri_Ene.Repository;
using Agri_Ene.Services;
using Agri_Ene.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Agri_Ene.Controllers
{
    // Specifies that only users can access actions in this controller
    [Authorize]
    public class ProductController : Controller
    {
        // Reference to the product repository interface
        private readonly IProductRepository _prodRepo;

        // Constructor to inject product repository instance
        public ProductController(IProductRepository prodRepo)
        {

            _prodRepo = prodRepo;           

        }
        //----------------------------------------------------------------------------//
        // Action method to show all products
        public async Task<IActionResult> Index()
        {
            IEnumerable<Product> products = await _prodRepo.GetAll();
            AgriUser farmer = await _prodRepo.GetFarmerDetails();

            // Create a view model for user details
            UserDetailsVM userDetails = new UserDetailsVM(
                farmer.FirstName,
                farmer.LastName,
                farmer.Email,
                farmer.PhoneNumber,
                products.ToList()
            );

            ViewData["Farmer"] = userDetails;
            // Return the view with products
            return View(products);
        }
        //----------------------------------------------------------------------------//

        // Action method to filter and list products
        public async Task<IActionResult> FilterList()
        {
            // Get all categories for the dropdown
            var categories = Enum.GetValues(typeof(ProductCategories))
                .Cast<ProductCategories>()
                .Select(category => _prodRepo.GetCategoryName(category))
                .ToList();

            // Initially, pass all products to the view
            IEnumerable<Product> products = await _prodRepo.GetAll();
            var productVMs = new List<EmployeeVM>();

            // Iterate through products to create view models
            foreach (Product product in products)
            {
                (string firstName, string lastName) = await _prodRepo.GetFarmer(product.prodId);
                string cat = _prodRepo.GetCategoryName(product.prodCategory);
                EmployeeVM vmObject = new EmployeeVM(firstName, lastName, product, cat);
                productVMs.Add(vmObject);
            }

            // Pass view models and categories to the view
            ViewBag.ProductVMs = productVMs;
            ViewBag.Categories = categories;

            return View(products);
        }
        //----------------------------------------------------------------------------//

        // Action method to get products by category, start date, and end date
        [HttpGet]
        public async Task<IActionResult> GetProductsByCategory(ProductCategories? category, DateTime? startDate, DateTime? endDate)
        {
            var products = await _prodRepo.GetAll();

            if (category.HasValue)
            {
                products = products.Where(p => p.prodCategory == category.Value);
            }

            if (startDate.HasValue && endDate.HasValue)
            {
                products = products.Where(p => p.productionDate >= startDate.Value && p.productionDate <= endDate.Value);
            }

            // Create view models for products
            var productVMs = new List<EmployeeVM>();
            foreach (Product product in products)
            {
                (string firstName, string lastName) = await _prodRepo.GetFarmer(product.prodId);
                string cat = _prodRepo.GetCategoryName(product.prodCategory);
                EmployeeVM vmObject = new EmployeeVM(firstName, lastName, product, cat);
                productVMs.Add(vmObject);
            }
            ViewBag.ProductVMs = productVMs;

            // Return partial view with product view models
            return PartialView("ProductList", productVMs);
        }
        //----------------------------------------------------------------------------//

        // Action method to show details of a single product
        public async Task<IActionResult> Details(int id)
        {
            var product = await _prodRepo.GetByIdAsync(id);
            (string firstName, string lastName) = await _prodRepo.GetFarmer(product.prodId);
            ViewData["SellerFirstName"] = firstName;
            ViewData["SellerLastName"] = lastName;

            return View(product);
        }

        // Action method to render create product view
        public IActionResult Create()
        {
            return View();
        }
        //----------------------------------------------------------------------------//

        // Action method to handle create product post request
        [HttpPost]
        public async Task<IActionResult> Create(Product prod)
        {
            if (!ModelState.IsValid)
            {
                return View(prod);
            }
            _prodRepo.Add(prod);
            return RedirectToAction("Index");
        }
        //----------------------------------------------------------------------------//
    }
}
//__---____---____---____---____---____---____---__.ooo END OF FILE ooo.__---____---____---____---____---____---____---__\\