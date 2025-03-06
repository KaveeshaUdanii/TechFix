using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using TechFixAPI.Models;

namespace TechFix_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        // POST api/product/add
        [HttpPost("add")]
        public async Task<IActionResult> AddProduct([FromBody] Product product)
        {
            if (product == null ||
    string.IsNullOrEmpty(product.Name) ||
    product.Stock <= 0 ||
    product.Price <= 0 ||
    string.IsNullOrEmpty(product.SID) ||
    string.IsNullOrEmpty(product.CID))
            {
                return BadRequest(new { message = "Please fill all required fields." });
            }


            // Get the last inserted PID (assuming format like "P01", "P02")
            var lastProduct = await _context.Product
                .OrderByDescending(p => p.PID)
                .FirstOrDefaultAsync();

            string newPid;
            if (lastProduct == null)
            {
                newPid = "P01"; // First entry
            }
            else
            {
                int lastNumber = int.Parse(lastProduct.PID.Substring(1)); // Extract number
                newPid = $"P{(lastNumber + 1):D2}"; // Format as "P02", "P03"
            }

            product.PID = newPid; // Assign new PID before saving

            _context.Product.Add(product);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Product added successfully!", productID = product.PID });
        }


    }
}
