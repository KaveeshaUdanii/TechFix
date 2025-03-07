using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechFixAPI.Models;

namespace TechFixAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SupplierController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddSupplier([FromBody] Supplier supplier)
        {
            if (supplier == null || string.IsNullOrEmpty(supplier.Name) ||
                string.IsNullOrEmpty(supplier.Email) || string.IsNullOrEmpty(supplier.ContactNo))
            {
                return BadRequest(new { message = "Please fill all required fields." });
            }

            // Get the last inserted SID (assuming format like "S01", "S02")
            var lastSupplier = await _context.Supplier
                .OrderByDescending(s => s.SID)
                .FirstOrDefaultAsync();

            string newSid;
            if (lastSupplier == null)
            {
                newSid = "S01"; // First entry
            }
            else
            {
                int lastNumber = int.Parse(lastSupplier.SID.Substring(1)); // Extract number
                newSid = $"S{(lastNumber + 1):D2}"; // Format as "S02", "S03"
            }

            supplier.SID = newSid; // Assign new SID before saving

            _context.Supplier.Add(supplier);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Supplier added successfully!", supplierId = supplier.SID });
        }

        
        // 1. Endpoint to get all suppliers
        [HttpGet("all")]
        public async Task<IActionResult> GetAllSuppliers()
        {
            // Fetch all suppliers from the database
            var suppliers = await _context.Supplier
                .Select(s => new
                {
                    s.SID,
                    s.Name,
                    s.Email,
                    s.ContactNo
                })
                .ToListAsync();

            // If no suppliers are found, return NotFound result
            if (suppliers == null || suppliers.Count == 0)
            {
                return NotFound(new { message = "No suppliers found." });
            }

            // Return the suppliers as JSON
            return Ok(suppliers);
        }
    }
}
