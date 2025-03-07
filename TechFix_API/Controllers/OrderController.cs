using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TechFixAPI.Models;

namespace TechFixAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OrderController(AppDbContext context)
        {
            _context = context;
        }

        // POST api/order/place-order
        [HttpPost("add")]
        public async Task<IActionResult> PlaceOrder([FromBody] Order order)
        {
            if (order == null || order.PID == null || order.SID == null || order.Quantity <= 0)
            {
                return BadRequest(new { message = "Please provide valid order details." });
            }

            // Get the last inserted OID (e.g., "O01", "O02")
            var lastOrder = await _context.Order
                .OrderByDescending(o => o.OID)
                .FirstOrDefaultAsync();

            string newOid;
            if (lastOrder == null)
            {
                newOid = "O01"; // First entry
            }
            else
            {
                int lastNumber = int.Parse(lastOrder.OID.Substring(1)); // Extract number
                newOid = $"O{(lastNumber + 1):D2}"; // Format as "O02", "O03"
            }

            // Assign new OID and set dates
            order.OID = newOid;
            order.OrderPlacedDate = DateTime.UtcNow; // Set order placed date

            _context.Order.Add(order);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Order placed successfully!", orderID = order.OID });
        }


        // GET api/order/total-orders
        [HttpGet("total-orders")]
        public async Task<IActionResult> GetTotalOrders()
        {
            try
            {
                int totalOrders = await _context.Order.CountAsync();
                return Ok(totalOrders);  // Return the total count as JSON
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }
        }



    }
}
