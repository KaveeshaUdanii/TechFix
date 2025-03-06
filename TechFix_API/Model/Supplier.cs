using System.ComponentModel.DataAnnotations;

namespace TechFixAPI.Models
{
    public class Supplier
    {
        [Key]
        public string? SID { get; set; } 

        [Required]
        public string Name { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string ContactNo { get; set; }

        
    }
}
