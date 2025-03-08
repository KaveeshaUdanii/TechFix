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



        // GET: api/suppliers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Supplier>>> GetSuppliers()
        {
            var suppliers = await _context.Supplier.ToListAsync();
            return Ok(suppliers);
        }

        // If needed, you can also have an endpoint to get a single supplier by SID
        [HttpGet("{id}")]
        public async Task<ActionResult<Supplier>> GetSupplier(string id)
        {
            var supplier = await _context.Supplier.FindAsync(id);

            if (supplier == null)
            {
                return NotFound();
            }

            return Ok(supplier);
        }

        // GET api/supplier/total-suppliers
        [HttpGet("total-suppliers")]
        public async Task<IActionResult> GetTotalSuppliers()
        {
            try
            {
                int totalSuppliers = await _context.Supplier.CountAsync();
                return Ok(totalSuppliers);  // Return total count as JSON
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }
        }

        [HttpGet("top-suppliers")]
        public async Task<IActionResult> GetTopSuppliers()
        {
            var topSuppliers = await _context.Order
                .GroupBy(o => o.SID)
                .Select(g => new
                {
                    SupplierID = g.Key,
                    TotalOrders = g.Count()
                })
                .OrderByDescending(s => s.TotalOrders)
                .Take(5)
                .Join(_context.Supplier, o => o.SupplierID, s => s.SID, (o, s) => new
                {
                    s.Name,
                    o.TotalOrders
                })
                .ToListAsync();

            return Ok(topSuppliers);
        }


        [HttpGet("get-supplier/{sid}")]
        public async Task<IActionResult> GetSupplierById(string sid)
        {
            if (string.IsNullOrEmpty(sid))
            {
                return BadRequest(new { message = "SID is required." });
            }

            var supplier = await _context.Supplier
                .Where(s => s.SID == sid)
                .FirstOrDefaultAsync();

            if (supplier == null)
            {
                return NotFound(new { message = "Supplier not found." });
            }

            return Ok(supplier);
        }

        [HttpPut("update-supplier/{sid}")]
        public async Task<IActionResult> UpdateSupplier(string sid, [FromBody] Supplier updatedSupplier)
        {
            if (updatedSupplier == null || sid != updatedSupplier.SID)
            {
                return BadRequest(new { message = "Invalid supplier data." });
            }

            var supplier = await _context.Supplier.FindAsync(sid);
            if (supplier == null)
            {
                return NotFound(new { message = "Supplier not found." });
            }

            // Update supplier details
            supplier.Name = updatedSupplier.Name;
            supplier.Email = updatedSupplier.Email;
            supplier.ContactNo = updatedSupplier.ContactNo;

            _context.Supplier.Update(supplier);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Supplier updated successfully!" });
        }


        [HttpDelete("delete-supplier/{sid}")]
        public async Task<IActionResult> DeleteSupplier(string sid)
        {
            var supplier = await _context.Supplier.FindAsync(sid);
            if (supplier == null)
            {
                return NotFound(new { message = "Supplier not found." });
            }

            _context.Supplier.Remove(supplier);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Supplier deleted successfully." });
        }


    }
}
