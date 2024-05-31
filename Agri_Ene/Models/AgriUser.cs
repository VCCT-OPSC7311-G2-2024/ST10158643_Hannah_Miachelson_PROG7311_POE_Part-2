using Microsoft.AspNetCore.Identity;

namespace Agri_Ene.Models
{
    //AgriUser Class that extends IdentityUser
    public class AgriUser : IdentityUser
    {
        
        // First name of the user
        public string? FirstName { get; set; }

        // Last name of the user
        public string? LastName { get; set; }

        // Collection of products associated with the user
        public ICollection<Product>? Products { get; set; }


    }
}
//__---____---____---____---____---____---____---__.ooo END OF FILE ooo.__---____---____---____---____---____---____---__\\