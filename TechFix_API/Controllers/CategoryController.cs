using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using TechFixAPI.Models; 

namespace TechFixAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }

        // POST api/category/add
        [HttpPost("add")]
        public async Task<IActionResult> AddCategory([FromBody] Category category)
        {
            if (category == null || string.IsNullOrEmpty(category.Name))
            {
                return BadRequest(new { message = "Please fill all required fields." });
            }

            // Get the last inserted SID (assuming format like "S01", "S02")
            var lastCategory = await _context.Category
                .OrderByDescending(s => s.CID)
                .FirstOrDefaultAsync();

            string newSid;
            if (lastCategory == null)
            {
                newSid = "C01"; // First entry
            }
            else
            {
                int lastNumber = int.Parse(lastCategory.CID.Substring(1)); // Extract number
                newSid = $"C{(lastNumber + 1):D2}"; // Format as "S02", "S03"
            }

            category.CID = newSid; // Assign new SID before saving

            _context.Category.Add(category);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Category added successfully!", categoryID = category.CID });
        }
    }

}
