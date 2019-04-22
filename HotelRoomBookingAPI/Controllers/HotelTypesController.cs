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
    public class HotelTypesController : ControllerBase
    {
        private readonly coreHotelRoomBookingFinalDatabaseContext _context;

        public HotelTypesController(coreHotelRoomBookingFinalDatabaseContext context)
        {
            _context = context;
        }

        // GET: api/HotelTypes
        [HttpGet]
        public IEnumerable<HotelTypes> GetHotelTypes()
        {
            return _context.HotelTypes;
        }

        // GET: api/HotelTypes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetHotelTypes([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var hotelTypes = await _context.HotelTypes.FindAsync(id);

            if (hotelTypes == null)
            {
                return NotFound();
            }

            return Ok(hotelTypes);
        }

        // PUT: api/HotelTypes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHotelTypes([FromRoute] int id, [FromBody] HotelTypes hotelTypes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != hotelTypes.HotelTypeId)
            {
                return BadRequest();
            }

            _context.Entry(hotelTypes).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HotelTypesExists(id))
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

        // POST: api/HotelTypes
        [HttpPost]
        public async Task<IActionResult> PostHotelTypes([FromBody] HotelTypes hotelTypes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.HotelTypes.Add(hotelTypes);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHotelTypes", new { id = hotelTypes.HotelTypeId }, hotelTypes);
        }

        // DELETE: api/HotelTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotelTypes([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var hotelTypes = await _context.HotelTypes.FindAsync(id);
            if (hotelTypes == null)
            {
                return NotFound();
            }

            _context.HotelTypes.Remove(hotelTypes);
            await _context.SaveChangesAsync();

            return Ok(hotelTypes);
        }

        private bool HotelTypesExists(int id)
        {
            return _context.HotelTypes.Any(e => e.HotelTypeId == id);
        }
    }
}