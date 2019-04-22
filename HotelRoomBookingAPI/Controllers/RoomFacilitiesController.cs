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
    public class RoomFacilitiesController : ControllerBase
    {
        private readonly coreHotelRoomBookingFinalDatabaseContext _context;

        public RoomFacilitiesController(coreHotelRoomBookingFinalDatabaseContext context)
        {
            _context = context;
        }

        // GET: api/RoomFacilities
        [HttpGet]
        public IEnumerable<RoomFacilities> GetRoomFacilities()
        {
            return _context.RoomFacilities;
        }

        // GET: api/RoomFacilities/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoomFacilities([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var roomFacilities = await _context.RoomFacilities.FindAsync(id);

            if (roomFacilities == null)
            {
                return NotFound();
            }

            return Ok(roomFacilities);
        }

        // PUT: api/RoomFacilities/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoomFacilities([FromRoute] int id, [FromBody] RoomFacilities roomFacilities)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != roomFacilities.RoomFacilityId)
            {
                return BadRequest();
            }

            _context.Entry(roomFacilities).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoomFacilitiesExists(id))
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

        // POST: api/RoomFacilities
        [HttpPost]
        public async Task<IActionResult> PostRoomFacilities([FromBody] RoomFacilities roomFacilities)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.RoomFacilities.Add(roomFacilities);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRoomFacilities", new { id = roomFacilities.RoomFacilityId }, roomFacilities);
        }

        // DELETE: api/RoomFacilities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoomFacilities([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var roomFacilities = await _context.RoomFacilities.FindAsync(id);
            if (roomFacilities == null)
            {
                return NotFound();
            }

            _context.RoomFacilities.Remove(roomFacilities);
            await _context.SaveChangesAsync();

            return Ok(roomFacilities);
        }

        private bool RoomFacilitiesExists(int id)
        {
            return _context.RoomFacilities.Any(e => e.RoomFacilityId == id);
        }
    }
}