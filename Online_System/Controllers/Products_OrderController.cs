using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Online_System.Data;
using Online_System.Models;

namespace Online_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Products_OrderController : ControllerBase
    {
        private readonly Online_Store_Context _context;

        public Products_OrderController(Online_Store_Context context)
        {
            _context = context;
        }

        // GET: api/Products_Order
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Products_Order>>> GetProducts_Orders()
        {
          if (_context.Products_Orders == null)
          {
              return NotFound();
          }
            return await _context.Products_Orders.ToListAsync();
        }

        // GET: api/Products_Order/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Products_Order>> GetProducts_Order(int id)
        {
          if (_context.Products_Orders == null)
          {
              return NotFound();
          }
            var products_Order = await _context.Products_Orders.FindAsync(id);

            if (products_Order == null)
            {
                return NotFound();
            }

            return products_Order;
        }

        // PUT: api/Products_Order/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducts_Order(int id, Products_Order products_Order)
        {
            if (id != products_Order.Product_Id)
            {
                return BadRequest();
            }

            _context.Entry(products_Order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Products_OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Products_Order
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Products_Order>> PostProducts_Order(Products_Order products_Order)
        {
          if (_context.Products_Orders == null)
          {
              return Problem("Entity set 'Online_Store_Context.Products_Orders'  is null.");
          }
            _context.Products_Orders.Add(products_Order);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (Products_OrderExists(products_Order.Product_Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetProducts_Order", new { id = products_Order.Product_Id }, products_Order);
        }

        // DELETE: api/Products_Order/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducts_Order(int id)
        {
            if (_context.Products_Orders == null)
            {
                return NotFound();
            }
            var products_Order = await _context.Products_Orders.FindAsync(id);
            if (products_Order == null)
            {
                return NotFound();
            }

            _context.Products_Orders.Remove(products_Order);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Products_OrderExists(int id)
        {
            return (_context.Products_Orders?.Any(e => e.Product_Id == id)).GetValueOrDefault();
        }
    }
}
