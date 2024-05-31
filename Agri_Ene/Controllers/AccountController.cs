using Agri_Ene.Data;
using Agri_Ene.Interface;
using Agri_Ene.Models;
using Agri_Ene.Services;
using Agri_Ene.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Agri_Ene.Controllers
{
    // Specifies that only users with the "farmer" role can access actions in this controller
    [Authorize(Roles = "farmer")] 
    public class AccountController : Controller
    {
        // Field to manage user data
        private readonly UserManager<AgriUser> _userManager;
        // Reference to the database context
        private readonly AppDbContext _context;
        // Reference to the product repository service
        private readonly IProductRepository _prodRepo;

        // Constructor to initialize the user manager and product repository 
        public AccountController(UserManager<AgriUser> userManager, IProductRepository prodRepo)
        {
            _userManager = userManager;
            _prodRepo = prodRepo;
        }

        // Action method to display index view
        public IActionResult Index()
        {
            return View();
        }
        //----------------------------------------------------------------------------//
        // Action method to display hub view
        public IActionResult Hub()
        {
            return View();
        }
        //----------------------------------------------------------------------------//
        // Action method to display market view
        public async Task<IActionResult> Market()
        {
            // Retrieving categories and products from the product repository
            var categories = Enum.GetValues(typeof(ProductCategories)).Cast<ProductCategories>()
                .Select(category => _prodRepo.GetCategoryName(category)).ToList();

            var productVMs = new List<EmployeeVM>();
            IEnumerable<Product> products = await _prodRepo.GetAllEmp();
            foreach (Product product in products)
            {
                (string firstName, string lastName) = await _prodRepo.GetFarmer(product.prodId);
                string cat = _prodRepo.GetCategoryName(product.prodCategory);
                EmployeeVM vmObject = new EmployeeVM(firstName, lastName, product, cat);
                productVMs.Add(vmObject);
            }

            // Retrieving farmer details from the product repository
            AgriUser farmer = await _prodRepo.GetFarmerDetails();

            // Passing data to views
            ViewData["Farmer"] = farmer;
            ViewBag.Categories = categories;

            // Returning market view with product view models
            return View(productVMs);
        }
        //----------------------------------------------------------------------------//
        // Action method to display education view
        public IActionResult Education()
        {
            return View();
        }
        //----------------------------------------------------------------------------//
        // Action method to display projects view
        public IActionResult Projects()
        {
            return View();
        }
        //----------------------------------------------------------------------------//
        // Action method to display details view of the authenticated user
        public async Task<IActionResult> Details()
        {
            var agriUser = await _userManager.GetUserAsync(User);
            if (agriUser == null)
            {
                return NotFound();
            }

            return View(agriUser);
        }
    }
}//__---____---____---____---____---____---____---__.ooo END OF FILE ooo.__---____---____---____---____---____---____---__\\