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

        // GET: api/Category/get-category/{cid}
        [HttpGet("get-category/{cid}")]
        public async Task<IActionResult> GetCategoryById(string cid)
        {
            var category = await _context.Category.FindAsync(cid);
            if (category == null)
            {
                return NotFound(new { message = "Category not found." });
            }

            return Ok(category);
        }

        // PUT: api/Category/update-category/{cid}
        [HttpPut("update-category/{cid}")]
        public async Task<IActionResult> UpdateCategory(string cid, [FromBody] Category updatedCategory)
        {
            // Ensure CID in the URL and the CID in the body are the same
            if (cid != updatedCategory.CID)
            {
                return BadRequest(new { message = "Category ID mismatch." });
            }

            // Find the existing category by CID
            var existingCategory = await _context.Category.FindAsync(cid);
            if (existingCategory == null)
            {
                return NotFound(new { message = "Category not found." });
            }

            // Update the properties
            existingCategory.Name = updatedCategory.Name;

            // Save the changes to the database
            _context.Category.Update(existingCategory);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Category updated successfully!" });
        }


        // DELETE: api/Category/delete-category/{cid}
        [HttpDelete("delete-category/{cid}")]
        public async Task<IActionResult> DeleteCategory(string cid)
        {
            // Find the category by CID
            var category = await _context.Category.FindAsync(cid);

            if (category == null)
            {
                return NotFound(new { message = "Category not found." });
            }

            // Remove the category from the database
            _context.Category.Remove(category);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Category deleted successfully!" });
        }


    }

}
