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
    public class ProductsController : Controller
    {

        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        //Get Products data
        [HttpGet]
        [Route("getProducts")]
        public async Task<IActionResult> GetProductList()
        {
            var productsList = _context.products.ToList();
            return productsList.Count > 0 ? Ok(productsList) : NotFound();
        }

        //Insert Products data
        [HttpPost]
        [Route("insertProduct")]
        public async Task<IActionResult> CreateProduct([FromBody] Product productData)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productData);
                await _context.SaveChangesAsync();
            }
            return Ok(productData);
        }

        //Deleat Products data
        [HttpDelete]
        [Route("deleatProduct")]
        public async Task<IActionResult> DeleteProduct(int? id)
        {
            if (ModelState.IsValid)
            {
                if (id == 0 || id < 0)
                {
                    return NotFound();
                }
                else
                {
                    var product = await _context.products.FindAsync(id);
                    if (product == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        _context.products.Remove(product);
                        await _context.SaveChangesAsync();
                        return Ok("Product Deleted");
                    }
                }
            }
            return BadRequest("Internal Server Error");
        }

        //Update Products data
        [HttpPut]
        [Route("updateProducts")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                    return Ok(string.Concat("Produc updated"));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.products.Any(e => e.Id == product.Id))
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
