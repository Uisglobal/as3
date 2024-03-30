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
    public class OrdersController : Controller
    {
        private readonly AppDbContext _context;

        public OrdersController(AppDbContext context)
        {
            _context = context;
        }

        //Get Orders data
        [HttpGet]
        [Route("getOrders")]
        public async Task<IActionResult> GetOrderList()
        {
            var orderList = _context.Orders.ToList();
            return orderList.Count > 0 ? Ok(orderList) : NotFound();
        }

        //Insert Orders data
        [HttpPost]
        [Route("insertOrder")]
        public async Task<IActionResult> CreateOrder([FromBody] Order orderData)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderData);
                await _context.SaveChangesAsync();
            }
            return Ok(orderData);
        }

        //Delete Orders data
        [HttpDelete]
        [Route("deleatOrder")]
        public async Task<IActionResult> DeleteOrder(int? id)
        {
            if (ModelState.IsValid)
            {
                if (id == 0 || id < 0)
                {
                    return NotFound();
                }
                else
                {
                    var order = await _context.Orders.FindAsync(id);
                    if (order == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        _context.Orders.Remove(order);
                        await _context.SaveChangesAsync();
                        return Ok("Order Deleted");
                    }
                }
            }
            return BadRequest("Internal Server Error");
        }

        //Update Orders data
        [HttpPut]
        [Route("updateOrder")]
        public async Task<IActionResult> UpdateOrder(int id, [FromBody] Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                    return Ok(string.Concat("Order updated"));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Orders.Any(e => e.Id == order.Id))
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
