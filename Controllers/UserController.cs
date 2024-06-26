﻿using System;
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
    public class UsersController : Controller
    {

        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        //Get Users data
        [HttpGet]
        [Route("getUsers")]
        public async Task<IActionResult> GetUserList()
        {
            var userList = _context.Users.ToList();
            return userList.Count > 0 ? Ok(userList) : NotFound();
        }

        //Insert Users data
        [HttpPost]
        [Route("insertUsers")]
        public async Task<IActionResult> CreateUser([FromBody] User userData)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userData);
                await _context.SaveChangesAsync();
            }
            return Ok(userData);
        }

        //Delete Users data
        [HttpDelete]
        [Route("deleteUser")]
        public async Task<IActionResult> DeleteUser(int? id)
        {
            if (ModelState.IsValid)
            {
                if (id == 0 || id < 0)
                {
                    return NotFound();
                }
                else
                {
                    var user = await _context.Users.FindAsync(id);
                    if (user == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        _context.Users.Remove(user);
                        await _context.SaveChangesAsync();
                        return Ok("User Deleted");
                    }
                }
            }
            return BadRequest("Internal Server Error");
        }

        //Update Users data
        [HttpPut]
        [Route("updateUser")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                    return Ok(string.Concat("User updated"));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Users.Any(e => e.Id == user.Id))
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
