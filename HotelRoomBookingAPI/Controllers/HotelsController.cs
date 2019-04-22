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
    public class HotelsController : ControllerBase
    {
        private readonly coreHotelRoomBookingFinalDatabaseContext _context;

        public HotelsController(coreHotelRoomBookingFinalDatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Hotels
        [HttpGet]
        public IEnumerable<Hotels> GetHotels()
        {
            return _context.Hotels;
        }

        // GET: api/Hotels/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetHotels([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var hotels = await _context.Hotels.FindAsync(id);

            if (hotels == null)
            {
                return NotFound();
            }

            return Ok(hotels);
        }

        // PUT: api/Hotels/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHotels([FromRoute] int id, [FromBody] Hotels hotels)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != hotels.HotelId)
            {
                return BadRequest();
            }

            _context.Entry(hotels).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HotelsExists(id))
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

        // POST: api/Hotels
        [HttpPost]
        public async Task<IActionResult> PostHotels([FromBody] Hotels hotels)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Hotels.Add(hotels);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHotels", new { id = hotels.HotelId }, hotels);
        }

        // DELETE: api/Hotels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotels([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var hotels = await _context.Hotels.FindAsync(id);
            if (hotels == null)
            {
                return NotFound();
            }

            _context.Hotels.Remove(hotels);
            await _context.SaveChangesAsync();

            return Ok(hotels);
        }

        private bool HotelsExists(int id)
        {
            return _context.Hotels.Any(e => e.HotelId == id);
        }
    }
}