using Agri_Ene.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Agri_Ene.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<AgriUser> _userManager;
        private readonly SignInManager<AgriUser> _signInManager;

        // Constructor to initialize the logger, user manager, and sign-in manager
        public HomeController(ILogger<HomeController> logger, UserManager<AgriUser> userManager, SignInManager<AgriUser> signInManager)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        //----------------------------------------------------------------------------//
        // Action method for rendering the index view
        public IActionResult Index()
        {
            return View();
        }
        //----------------------------------------------------------------------------//
        // Action method for rendering the privacy view
        public IActionResult Privacy()
        {
            return View();
        }
        //----------------------------------------------------------------------------//
        // Action method for handling errors
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        //----------------------------------------------------------------------------//
        // Action method for getting user role and redirecting accordingly
        [HttpPost]
        public async Task<IActionResult> GetUserRole()
        {
            if (_signInManager.IsSignedIn(User))
            {
                var user = await _userManager.GetUserAsync(User);
                var roles = await _userManager.GetRolesAsync(user);

                //  Check if user is a farmer and redirect to Account/Index for farmers
                if (roles.Contains("farmer"))
                {
                    return RedirectToAction("Index", "Account");
                }
                // Check if user is an employee and redirect to Employee/Index for employees
                else if (roles.Contains("employee"))
                {
                    return RedirectToAction("Index", "Employee");
                }
                // Default action if role is not matched
                else
                {
                    return RedirectToAction("Index", "Home"); 
                }
            } // Redirect to home if user is not signed in
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        //----------------------------------------------------------------------------//
    }
}
//__---____---____---____---____---____---____---__.ooo END OF FILE ooo.__---____---____---____---____---____---____---__\\