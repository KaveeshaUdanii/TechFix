
using System.ComponentModel.DataAnnotations;

namespace TechFixAPI.Models
{
    public class Order
    {
        [Key]
        public string? OID { get; set; } // Order ID
        public string PID { get; set; } // Product ID (Foreign Key)
        public int Quantity { get; set; } // Quantity of the product
        public string SID { get; set; } // Supplier ID (Foreign Key)
        public DateTime OrderDate { get; set; } // Date when the order was placed
        public DateTime OrderPlacedDate { get; set; } // Date when the order was confirmed by admin

    }
}
