using System.ComponentModel.DataAnnotations;

namespace TechFixAPI.Models
{
    public class Category
    {
        [Key]
        public string? CID { get; set; }

        [Required]
        public string Name { get; set; }

        
    }
}