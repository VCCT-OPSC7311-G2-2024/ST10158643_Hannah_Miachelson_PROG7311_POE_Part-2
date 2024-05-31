using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Agri_Ene.Data;

namespace Agri_Ene.Models
{
    public class Product
    {
        // Product Primary Key 
        [Key]
        public int? prodId { get; set; }

        // Name of the product
        public string? prodName { get; set; }

        // Description of the product
        public string? prodDescription { get; set; }

        // Category of the product
        public ProductCategories prodCategory { get; set; }

        // Production date of the product
        public DateTime? productionDate { get; set; }

        // Foreign key property to link to the farmer
        public string? FarmerId { get; set; }

        // Navigation property to access the associated farmer
        [ForeignKey("FarmerId")]
        public AgriUser? Farmer { get; set; }
    }

}//__---____---____---____---____---____---____---__.ooo END OF FILE ooo.__---____---____---____---____---____---____---__\\
