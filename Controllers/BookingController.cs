using AgileAPIAT2.Services;
using Microsoft.AspNetCore.Mvc;
using AgileAPIAT2.Models;

namespace AgileAPIAT2.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    // this is the book controller class 
    public class BookingController : ControllerBase
    {
        private readonly BookingService _bookingService;

        public BookingController(BookingService bookingService)
        {
            _bookingService = bookingService;
        }

        // Method for retrieving all (hard coded limit of 10) bookings.
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int skip = 0, [FromQuery] int limit = 10)
        {
            var booking = await _bookingService.GetAllAsync(skip, limit);
            return Ok(booking);
        }

        // Method for retrieving a booking by its booking ID.
        [HttpGet("by-booking-id/{bookingId}")]
        public async Task<IActionResult> GetByBookingId(string bookingId, [FromQuery] int limit = 24)
        {
            var results = await _bookingService.GetByBookingIdAsync(bookingId, limit);
            return results.Any() ? Ok(results) : NotFound();
        }


        // Method for retrieving a booking by its MongoDB ID.
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var booking = await _bookingService.GetByIdAsync(id);
            if (booking == null)
                return NotFound();
            return Ok(booking);
        }


        // Method for retrieving a booking by its size.
        [HttpGet("size/{size:int}")]
        public async Task<IActionResult> GetBySize(int size)
        {
            var booking = await _bookingService.GetBySizeAsync(size);
            if (booking == null)
                return NotFound();
            return Ok(booking);
        }

        // Post method for posting a booking 
        [HttpPost]
        public async Task<IActionResult> Create(string bookingId, int size, DateTime date)
        {
            await _bookingService.CreateAsync(bookingId, size, date);
            return Ok();
        }


        //  Put method for updating
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, int size, DateTime date)
        {
            var existing = await _bookingService.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            await _bookingService.UpdateAsync(id, size, date);
            return NoContent();
        }

        // delete methid 
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
