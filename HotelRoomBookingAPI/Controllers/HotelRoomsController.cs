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
    public class HotelRoomsController : ControllerBase
    {
        private readonly coreHotelRoomBookingFinalDatabaseContext _context;

        public HotelRoomsController(coreHotelRoomBookingFinalDatabaseContext context)
        {
            _context = context;
        }

        // GET: api/HotelRooms
        [HttpGet]
        public IEnumerable<HotelRooms> GetHotelRooms()
        {
            return _context.HotelRooms;
        }

        // GET: api/HotelRooms/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetHotelRooms([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var hotelRooms = await _context.HotelRooms.FindAsync(id);

            if (hotelRooms == null)
            {
                return NotFound();
            }

            return Ok(hotelRooms);
        }

        // PUT: api/HotelRooms/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHotelRooms([FromRoute] int id, [FromBody] HotelRooms hotelRooms)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != hotelRooms.RoomId)
            {
                return BadRequest();
            }

            _context.Entry(hotelRooms).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HotelRoomsExists(id))
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

        // POST: api/HotelRooms
        [HttpPost]
        public async Task<IActionResult> PostHotelRooms([FromBody] HotelRooms hotelRooms)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.HotelRooms.Add(hotelRooms);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHotelRooms", new { id = hotelRooms.RoomId }, hotelRooms);
        }

        // DELETE: api/HotelRooms/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotelRooms([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var hotelRooms = await _context.HotelRooms.FindAsync(id);
            if (hotelRooms == null)
            {
                return NotFound();
            }

            _context.HotelRooms.Remove(hotelRooms);
            await _context.SaveChangesAsync();

            return Ok(hotelRooms);
        }

        private bool HotelRoomsExists(int id)
        {
            return _context.HotelRooms.Any(e => e.RoomId == id);
        }
    }
}