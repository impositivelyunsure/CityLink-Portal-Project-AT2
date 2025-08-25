using AgileAPIAT2.Services;
using Microsoft.AspNetCore.Mvc;
using AgileAPIAT2.Models;

namespace AgileAPIAT2.Controllers
{
   
        [ApiController]
        [Route("api/[controller]")]
        public class BookingController : ControllerBase
        {
            private readonly BookingService _bookingService;

            public BookingController(BookingService restaurantService)
            {
                _bookingService = restaurantService;
            }

            [HttpGet]
            public async Task<IActionResult> GetAll([FromQuery] int skip = 0, [FromQuery] int limit = 10)
            {
                var booking = await _bookingService.GetAllAsync(skip, limit);
                return Ok(booking);
            }


            [HttpGet("{id:length(24)}")]
            public async Task<IActionResult> GetById(string id)
            {
                var booking = await _bookingService.GetByIdAsync(id);
                if (booking == null)
                    return NotFound();
                return Ok(booking);
            }

            [HttpPost]
            public async Task<IActionResult> Create(Booking booking)
            {
                await _bookingService.CreateAsync(booking);
                return CreatedAtAction(nameof(GetById), new { id = booking.Id }, booking);
            }


            [HttpPut("{id:length(24)}")]
            public async Task<IActionResult> Update(string id, Booking booking)
            {
                var existing = await _bookingService.GetByIdAsync(id);
                if (existing == null)
                    return NotFound();

            booking.Id = id;
                await _bookingService.UpdateAsync(id, booking);
                return NoContent();
            }

            [HttpDelete("{id:length(24)}")]
            public async Task<IActionResult> Delete(string id)
            {
                var booking = await _bookingService.GetByIdAsync(id);
                if (booking == null)
                    return NotFound();

                await _bookingService.DeleteAsync(id);
                return NoContent();
            }
        }
    }
