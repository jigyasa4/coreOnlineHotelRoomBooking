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
    public class BookingRecordsController : ControllerBase
    {
        private readonly coreHotelRoomBookingFinalDatabaseContext _context;

        public BookingRecordsController(coreHotelRoomBookingFinalDatabaseContext context)
        {
            _context = context;
        }

        // GET: api/BookingRecords
        [HttpGet]
        public IEnumerable<BookingRecords> GetBookingRecords()
        {
            return _context.BookingRecords;
        }

        // GET: api/BookingRecords/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookingRecords([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var bookingRecords = await _context.BookingRecords.FindAsync(id);

            if (bookingRecords == null)
            {
                return NotFound();
            }

            return Ok(bookingRecords);
        }

        // PUT: api/BookingRecords/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBookingRecords([FromRoute] int id, [FromBody] BookingRecords bookingRecords)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bookingRecords.BookingId)
            {
                return BadRequest();
            }

            _context.Entry(bookingRecords).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookingRecordsExists(id))
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

        // POST: api/BookingRecords
        [HttpPost]
        public async Task<IActionResult> PostBookingRecords([FromBody] BookingRecords bookingRecords)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.BookingRecords.Add(bookingRecords);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (BookingRecordsExists(bookingRecords.BookingId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetBookingRecords", new { id = bookingRecords.BookingId }, bookingRecords);
        }

        // DELETE: api/BookingRecords/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookingRecords([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var bookingRecords = await _context.BookingRecords.FindAsync(id);
            if (bookingRecords == null)
            {
                return NotFound();
            }

            _context.BookingRecords.Remove(bookingRecords);
            await _context.SaveChangesAsync();

            return Ok(bookingRecords);
        }

        private bool BookingRecordsExists(int id)
        {
            return _context.BookingRecords.Any(e => e.BookingId == id);
        }
    }
}