using Agri_Ene.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Agri_Ene.Areas.Identity.Pages
{
    [Authorize]
    public class UserModel : PageModel
    {
     
        private readonly UserManager<AgriUser> _userManager;
        //object to hold user data 
        public AgriUser? AgriUser;
        public UserModel(UserManager<AgriUser> userManger)
        {
            _userManager = userManger;
        }
        public void OnGet()
        {
            var task =_userManager.GetUserAsync(User);
            task.Wait();
            AgriUser = task.Result;
            //return View(AgriUser);
        }
    }
}
