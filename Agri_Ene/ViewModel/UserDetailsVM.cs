using Agri_Ene.Models;

namespace Agri_Ene.ViewModel
{
    // ViewModel Class for displaying user-related information
    public class UserDetailsVM
    {// First name of the user
        public string? FirstName { get; set; }

        // Last name of the user
        public string? LastName { get; set; }

        // Email address of the user
        public string? Email { get; set; }

        // Phone number of the user
        public string? PhoneNumber { get; set; }

        // Collection of products associated with the user
        public ICollection<Product>? Products { get; set; }

        // Constructor to initialize user details with associated products
        public UserDetailsVM(string firstName, string lastName, string email, string phoneNumber, ICollection<Product>? products)
        {
            this.FirstName = firstName;    
            this.LastName = lastName;       
            this.Email = email;            
            this.PhoneNumber = phoneNumber; 
            this.Products = products;       
        }
       //----------------------------------------------------------------------------//
    }
}
//__---____---____---____---____---____---____---__.ooo END OF FILE ooo.__---____---____---____---____---____---____---__\\