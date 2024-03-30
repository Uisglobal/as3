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
    public class CommentsController : Controller
    {
        private readonly AppDbContext _context;

        public CommentsController(AppDbContext context)
        {
            _context = context;
        }

        // get comments data

        [HttpGet]
        [Route("getComments")]
        public async Task<IActionResult> GetCommentList()
        {
            var commentList = _context.comment.ToList();
            return commentList.Count > 0 ? Ok(commentList) : NotFound();
        }

        // post comments data
        [HttpPost]
        [Route("insertComment")]
        public async Task<IActionResult> CreateComment([FromBody] Comment commentData)
        {
            if (ModelState.IsValid)
            {
                _context.Add(commentData);
                await _context.SaveChangesAsync();
            }
            return Ok(commentData);
        }

        // Deleate comments data
        [HttpDelete]
        [Route("deleatComment")]
        public async Task<IActionResult> DeleteComment(int? id)
        {
            if (ModelState.IsValid)
            {
                if (id == 0 || id < 0)
                {
                    return NotFound();
                }
                else
                {
                    var comment = await _context.comment.FindAsync(id);
                    if (comment == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        _context.comment.Remove(comment);
                        await _context.SaveChangesAsync();
                        return Ok("Comment Deleted");
                    }
                }
            }
            return BadRequest("Internal Server Error");
        }

        //Update comments data
        [HttpPut]
        [Route("updateComment")]
        public async Task<IActionResult> UpdateComment(int id, [FromBody] Comment comment)
        {
            if (id != comment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comment);
                    await _context.SaveChangesAsync();
                    return Ok(string.Concat("comment updated"));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.comment.Any(e => e.Id == comment.Id))
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
