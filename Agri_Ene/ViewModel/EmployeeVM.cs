using Agri_Ene.Models;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Agri_Ene.ViewModel
{ 
    // ViewModel Class for displaying employee-related information
    public class EmployeeVM
    {
        // First name of the employee
        public string firstName { get; set; }

        // Last name of the employee
        public string lastName { get; set; }

        // Product associated with the employee
        public Product product { get; set; }

        // Category of the product associated with the employee
        public string productCategory { get; set; }

        // Constructor to initialize the employee view model with provided values
        public EmployeeVM(string fName, string lName, Product product, string cat)
        {
            // Initialize the first name property
            this.firstName = fName;

            // Initialize the last name property
            this.lastName = lName;

            // Initialize the product property
            this.product = product;

            // Initialize the product category property
            this.productCategory = cat;
        }
        //----------------------------------------------------------------------------//
    }
}
//__---____---____---____---____---____---____---__.ooo END OF FILE ooo.__---____---____---____---____---____---____---__\\