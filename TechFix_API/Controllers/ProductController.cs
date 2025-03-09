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

        // GET api/product/total-products
        [HttpGet("total-products")]
        public async Task<IActionResult> GetTotalProducts()
        {
            try
            {
                int totalProducts = await _context.Product.CountAsync();
                return Ok(totalProducts);  // Return total count as JSON
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }
        }

        [HttpGet("top-products")]
        public async Task<IActionResult> GetTopOrderedProducts()
        {
            var topProducts = await _context.Order
                .GroupBy(o => o.PID)
                .Select(g => new
                {
                    ProductID = g.Key,
                    TotalQuantity = g.Sum(o => o.Quantity)
                })
                .OrderByDescending(p => p.TotalQuantity)
                .Take(5)
                .Join(_context.Product, o => o.ProductID, p => p.PID, (o, p) => new
                {
                    p.Name,
                    o.TotalQuantity
                })
                .ToListAsync();

            return Ok(topProducts);
        }



        // GET: api/Product/get-product/{pid}
        [HttpGet("get-product/{pid}")]
        public async Task<IActionResult> GetProductById(string pid)
        {
            // Find the product by PID
            var product = await _context.Product.FirstOrDefaultAsync(p => p.PID == pid);

            if (product == null)
            {
                return NotFound(new { message = "Product not found." });
            }

            // Return product details
            return Ok(product);
        }

        [HttpPut("update/{pid}")]
        public async Task<IActionResult> UpdateProduct(string pid, [FromBody] Product product)
        {
            var existingProduct = await _context.Product.FindAsync(pid);
            if (existingProduct == null)
            {
                return NotFound();
            }

            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;
            existingProduct.Stock = product.Stock;
            existingProduct.Description = product.Description;
            existingProduct.CID = product.CID;
            existingProduct.SID = product.SID;

            await _context.SaveChangesAsync();

            return Ok(existingProduct);
        }

        [HttpDelete("delete/{productId}")]
        public async Task<IActionResult> DeleteProduct(string productId)
        {
            // Find the product by PID
            var product = await _context.Product.FirstOrDefaultAsync(p => p.PID == productId);

            if (product == null)
            {
                return NotFound(new { message = "Product not found." });
            }

            // Remove the product from the database
            _context.Product.Remove(product);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Product deleted successfully." });
        }


        [HttpGet("lowest-stock-products")]
        public async Task<IActionResult> GetLowestStockProducts()
        {
            var lowestStockProducts = await _context.Product
                .OrderBy(p => p.Stock)  // Order by stock in ascending order
                .Take(5)  // Get the top 5 products with the lowest stock
                .Select(p => new
                {
                    p.Name,
                    p.Stock
                })
                .ToListAsync();

            return Ok(lowestStockProducts);
        }


    }
}
