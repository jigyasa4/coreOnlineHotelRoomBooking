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
    public class FeedbacksController : ControllerBase
    {
        private readonly coreHotelRoomBookingFinalDatabaseContext _context;

        public FeedbacksController(coreHotelRoomBookingFinalDatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Feedbacks
        [HttpGet]
        public IEnumerable<Feedbacks> GetFeedbacks()
        {
            return _context.Feedbacks;
        }

        // GET: api/Feedbacks/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFeedbacks([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var feedbacks = await _context.Feedbacks.FindAsync(id);

            if (feedbacks == null)
            {
                return NotFound();
            }

            return Ok(feedbacks);
        }

        // PUT: api/Feedbacks/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFeedbacks([FromRoute] int id, [FromBody] Feedbacks feedbacks)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != feedbacks.FeedbackId)
            {
                return BadRequest();
            }

            _context.Entry(feedbacks).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FeedbacksExists(id))
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

        // POST: api/Feedbacks
        [HttpPost]
        public async Task<IActionResult> PostFeedbacks([FromBody] Feedbacks feedbacks)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Feedbacks.Add(feedbacks);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFeedbacks", new { id = feedbacks.FeedbackId }, feedbacks);
        }

        // DELETE: api/Feedbacks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFeedbacks([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var feedbacks = await _context.Feedbacks.FindAsync(id);
            if (feedbacks == null)
            {
                return NotFound();
            }

            _context.Feedbacks.Remove(feedbacks);
            await _context.SaveChangesAsync();

            return Ok(feedbacks);
        }

        private bool FeedbacksExists(int id)
        {
            return _context.Feedbacks.Any(e => e.FeedbackId == id);
        }
    }
}