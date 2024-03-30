using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Assignment_3_APIs.Data;
using Assignment_3_APIs.Models;

namespace Assignment_3_APIs.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    // endpoint
    public class CartsController : Controller
    {
        private readonly AppDbContext _context;

        public CartsController(AppDbContext context)
        {
            _context = context;
        }
        // get cart data
        [HttpGet]
        [Route("GetCarts")]
        public async Task<IActionResult> GetCartList()
        {
            var cartList = _context.Carts.ToList();
            return cartList.Count > 0 ? Ok(cartList) : NotFound();
        }

        // post cart data
        [HttpPost]
        [Route("insertCart")]
        public async Task<IActionResult> CreateCart([FromBody] Cart cartData)
        {
            if (ModelState.IsValid)
            {
             _context.Add(cartData);
        await _context.SaveChangesAsync();
        return Ok(cartData);
            }
            return Ok(cartData);
        }

        // delete cart data
        [HttpDelete]
        [Route("deleteCart")]
        public async Task<IActionResult> DeleteCart(int? id)
        {
            if (ModelState.IsValid)
            {
                if (id == 0 || id < 0)
                {
                    return NotFound();
                }
                else
                {
                    var cart = await _context.Carts.FindAsync(id);
                    if (cart == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        _context.Carts.Remove(cart);
                        await _context.SaveChangesAsync();
                        return Ok("Cart Deleted");
                    }
                }
            }
            return BadRequest("Internal Server Error");
        }
        // update cart data
        [HttpPut]
        [Route("UpdateCart")]
        public async Task<IActionResult> UpdateCart(int id, [FromBody] Cart cart)
        {
            if (id != cart.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cart);
                    await _context.SaveChangesAsync();
                    return Ok(string.Concat("Cart updated"));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Carts.Any(e => e.Id == cart.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return BadRequest("Internal Server Error");
        }


    }
}
