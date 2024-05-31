using Agri_Ene.Data;
using Agri_Ene.Interface;
using Agri_Ene.Models;
using Agri_Ene.Services;
using Agri_Ene.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using static Agri_Ene.Controllers.ProductController;

namespace Agri_Ene.Controllers
{
    // Specifies that only users with the "employee" role can access actions in this controller
    [Authorize(Roles = "employee")]
    public class EmployeeController : Controller
    { 
        // Field to manage user data
        private readonly UserManager<AgriUser> _userManager;
        //Reference to database 
        private readonly AppDbContext _context;
        //Reference to the Services for the Product
        private readonly IProductRepository _prodRepo;

        // Constructor to initialize the user manager and product repository 
        public EmployeeController (UserManager<AgriUser> userManager, IProductRepository prodRepo)
        {
            _userManager = userManager;
            _prodRepo = prodRepo;
        }
        //----------------------------------------------------------------------------//
        // Action method to display index view
        public async Task<IActionResult> IndexAsync()
        {
            var agriUser = await _userManager.GetUserAsync(User);
            if (agriUser == null)
            {
                return NotFound();
            }
            return View(agriUser);
        }
        //----------------------------------------------------------------------------//
        // Action method to display employee filter list view
        public async Task<IActionResult> EmpFilterList()
        {
            // Get all categories for the dropdown
            var categories = Enum.GetValues(typeof(ProductCategories)).Cast<ProductCategories>()
              .Select(category => _prodRepo.GetCategoryName(category)).ToList();

            // Initially, pass all products to the view
            IEnumerable<Product> products = await _prodRepo.GetAllEmp();

            var productVMs = new List<EmployeeVM>();
            foreach (Product product in products)
            {
                (string firstName, string lastName) = await _prodRepo.GetFarmer(product.prodId);
                string cat = _prodRepo.GetCategoryName(product.prodCategory);
                EmployeeVM vmObject = new EmployeeVM(firstName, lastName, product, cat);
                productVMs.Add(vmObject);
            }

            ViewBag.ProductVMs = productVMs;
            ViewBag.Categories = categories;

            return View(products);
        }
        //----------------------------------------------------------------------------//
        // Action method to filter products by category, start date, end date
        [HttpGet]
        public async Task<IActionResult> GetAllProductsByCategory(ProductCategories? category, DateTime? startDate, DateTime? endDate)
        {
            var products = await _prodRepo.GetAllEmp();

            if (category.HasValue)
            {
                products = products.Where(p => p.prodCategory == category.Value);
            }

            if (startDate.HasValue && endDate.HasValue)
            {
                products = products.Where(p => p.productionDate >= startDate.Value && p.productionDate <= endDate.Value);
            }

            var productVMs = new List<EmployeeVM>();
            foreach (Product product in products)
            {
                (string firstName, string lastName) = await _prodRepo.GetFarmer(product.prodId);
                string cat = _prodRepo.GetCategoryName(product.prodCategory);
                EmployeeVM vmObject = new EmployeeVM(firstName, lastName, product, cat);
                productVMs.Add(vmObject);
            }
            ViewBag.ProductVMs = productVMs;

            return PartialView("EmpProductList", productVMs);

        }

        // Action method to display details view of the authenticated user
        [Authorize]
        public async Task<IActionResult> Details()
        {
            var agriUser = await _userManager.GetUserAsync(User);
            if (agriUser == null)
            {
                return NotFound();
            }
            return View(agriUser);
        }
        //----------------------------------------------------------------------------//
        // Action method to display farmers view
        public async Task<IActionResult> Farmers()
        {
            var userDetailsList = new List<UserDetailsVM>();

            var farmers = await _userManager.GetUsersInRoleAsync("Farmer");
            foreach (var farmer in farmers)
            {
                // Retrieve the list of products for the current farmer
                var productList = await _prodRepo.GetByFarmer(farmer.FirstName);

                // Create a new UserDetailsVM object for the current farmer
                UserDetailsVM userDetails = new UserDetailsVM(
                    farmer.FirstName,
                    farmer.LastName,
                    farmer.Email,
                    farmer.PhoneNumber,
                    productList.ToList() // Convert the IEnumerable<Product> to List<Product>
                );

                userDetailsList.Add(userDetails);
            }

            if (userDetailsList.Count == 0)
            {
                return NotFound();
            }

            return View(userDetailsList);
        }
        //----------------------------------------------------------------------------//
        // Action method to display the list of products associated with a specific farmer
        public async Task<IActionResult> FarmerList(string farmerName)
        {
            // Get all categories for the dropdown
            var categories = Enum.GetValues(typeof(ProductCategories))
                .Cast<ProductCategories>()
                .Select(category => _prodRepo.GetCategoryName(category))
                .ToList();

            // Retrieve products associated with the specified farmer
            IEnumerable<Product> products = await _prodRepo.GetByFarmer(farmerName);
            var productVMs = new List<EmployeeVM>();

            // Map products to view models
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
        // Action method to filter products by category, start date, end date, and farmer name
        [HttpGet]
        public async Task<IActionResult> GetProductsByCategory(ProductCategories? category, DateTime? startDate, DateTime? endDate, string farmerName)
        {
            // Retrieve products associated with the specified farmer
            var products = await _prodRepo.GetByFarmer(farmerName);

            // Filter products based on category and date range if provided
            if (category.HasValue)
            {
                products = products.Where(p => p.prodCategory == category.Value);
            }

            if (startDate.HasValue && endDate.HasValue)
            {
                products = products.Where(p => p.productionDate >= startDate.Value && p.productionDate <= endDate.Value);
            }

            var productVMs = new List<EmployeeVM>();

            // Map filtered products to view models
            foreach (Product product in products)
            {
                (string firstName, string lastName) = await _prodRepo.GetFarmer(product.prodId);
                string cat = _prodRepo.GetCategoryName(product.prodCategory);
                EmployeeVM vmObject = new EmployeeVM(firstName, lastName, product, cat);
                productVMs.Add(vmObject);
            }

            // Pass view models to the partial view
            ViewBag.ProductVMs = productVMs;

            return PartialView("FarmerProductList", productVMs);
        }
    }
}
//__---____---____---____---____---____---____---__.ooo END OF FILE ooo.__---____---____---____---____---____---____---__\\

