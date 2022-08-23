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
    public class CartController : ControllerBase

    {
        private readonly Online_Store_Context _context;
        public CartController(Online_Store_Context context)
        {
            _context = context;
        }
        [HttpGet]

        public async Task<ActionResult<IEnumerable<Cart>>> GetAllCarts()
        {
            if (_context.Carts == null)
            {
                return NotFound();
            }
            return await _context.Carts.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Cart>>> GetCartProducts(int id)
        {
            if (_context.Carts == null)
            {
                return NotFound();
            }
            var carts = await _context.Carts.Where(a => a.User_Id == id).ToListAsync();

            if (carts == null)
            {
                return NotFound();
            }

            return carts;
        }
        [HttpPost]
        public async Task<ActionResult<Cart>> PostCartElement(Cart cart)
        {
            if (_context.Carts == null)
            {
                return Problem("Entity set 'Online_Store_Context.Carts'  is null.");
            }
            _context.Carts.Add(cart);
            await _context.SaveChangesAsync();
            return Created("ay 7aga", cart);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserCart(int id)
        {
            if (_context.Carts == null)
            {
                return NotFound();
            }
            var carts = await _context.Carts.Where(a => a.User_Id == id).ToListAsync();

            if (carts == null)
            {
                return NotFound();
            }
            foreach (var c in carts)
            {
                _context.Carts.Remove(c);
            }

            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpDelete("{u_id}/{p_id}")]
       

        public async Task<IActionResult> DeleteOneProductFromCart(int u_id,int p_id) 
        {
            if (_context.Carts == null)
            {
                return NotFound();
            }
            var cart = await _context.Carts.Where(a => a.Product_Id == p_id && a.User_Id == u_id).FirstOrDefaultAsync();
            if (cart == null)
            {
                return NotFound();
            }

            _context.Carts.Remove(cart);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpPut("{u_id}/{p_id}")]
        public async Task<IActionResult> EditQuantity(int u_id,int p_id , Cart cart)
        {
                if (u_id != cart.User_Id || p_id!=cart.Product_Id)
            {
                return BadRequest();
            }
            var old = await _context.Carts.Where(a => a.Product_Id == p_id && a.User_Id == u_id).FirstOrDefaultAsync();
            try
            {
             old.Quantity+= cart.Quantity;
            _context.Entry(old).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            }

           
            catch (DbUpdateConcurrencyException)
            {
                if (old==null)
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
     
    }
}
