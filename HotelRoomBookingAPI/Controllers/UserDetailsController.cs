using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelRoomBookingAPI.Models;
using Microsoft.AspNetCore.Cors;

namespace HotelRoomBookingAPI.Controllers
{
    [EnableCors("AllowMyOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserDetailsController : ControllerBase
    {
        private readonly coreHotelRoomBookingFinalDatabaseContext _context;

        public UserDetailsController(coreHotelRoomBookingFinalDatabaseContext context)
        {
            _context = context;
        }

        // GET: api/UserDetails
        [HttpGet]
        public IEnumerable<UserDetails> GetUserDetails()
        {
            return _context.UserDetails;
        }

        // GET: api/UserDetails/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserDetails([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userDetails = await _context.UserDetails.FindAsync(id);

            if (userDetails == null)
            {
                return NotFound();
            }

            return Ok(userDetails);
        }

        // PUT: api/UserDetails/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserDetails([FromRoute] int id, [FromBody] UserDetails userDetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userDetails.UserId)
            {
                return BadRequest();
            }

            _context.Entry(userDetails).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserDetailsExists(id))
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

        // POST: api/UserDetails
        [HttpPost]
        public async Task<IActionResult> PostUserDetails([FromBody] UserDetails userDetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.UserDetails.Add(userDetails);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserDetails", new { id = userDetails.UserId }, userDetails);
        }

        // DELETE: api/UserDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserDetails([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userDetails = await _context.UserDetails.FindAsync(id);
            if (userDetails == null)
            {
                return NotFound();
            }

            _context.UserDetails.Remove(userDetails);
            await _context.SaveChangesAsync();

            return Ok(userDetails);
        }

        private bool UserDetailsExists(int id)
        {
            return _context.UserDetails.Any(e => e.UserId == id);
        }
    }
}