using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechFixAPI.Models
{
    public class Product
    {
        [Key]
        public string PID { get; set; }  // Primary Key

        [Required]
        public string Name { get; set; }  // Product Name

        [Required]
        public int Stock { get; set; }  // Stock quantity

        [Required]
        public int Price { get; set; }  // Product price

        [Required]
        public string SID { get; set; }  // Composite Primary Key part (SID)

        public string Description { get; set; }  // Product description

        [Required]
        public string CID { get; set; }  // Foreign Key to Category Table

        
    }

}
