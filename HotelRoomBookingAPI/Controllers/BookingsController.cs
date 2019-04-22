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
    public class BookingsController : ControllerBase
    {
        private readonly coreHotelRoomBookingFinalDatabaseContext _context;

        public BookingsController(coreHotelRoomBookingFinalDatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Bookings
        [HttpGet]
        public IEnumerable<Bookings> GetBookings()
        {
            return _context.Bookings;
        }

        // GET: api/Bookings/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookings([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var bookings = await _context.Bookings.FindAsync(id);

            if (bookings == null)
            {
                return NotFound();
            }

            return Ok(bookings);
        }

        // PUT: api/Bookings/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBookings([FromRoute] int id, [FromBody] Bookings bookings)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bookings.BookingId)
            {
                return BadRequest();
            }

            _context.Entry(bookings).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookingsExists(id))
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

        // POST: api/Bookings
        [HttpPost]
        public async Task<IActionResult> PostBookings([FromBody] Bookings bookings)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Bookings.Add(bookings);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBookings", new { id = bookings.BookingId }, bookings);
        }

        // DELETE: api/Bookings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookings([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var bookings = await _context.Bookings.FindAsync(id);
            if (bookings == null)
            {
                return NotFound();
            }

            _context.Bookings.Remove(bookings);
            await _context.SaveChangesAsync();

            return Ok(bookings);
        }

        private bool BookingsExists(int id)
        {
            return _context.Bookings.Any(e => e.BookingId == id);
        }
    }
}