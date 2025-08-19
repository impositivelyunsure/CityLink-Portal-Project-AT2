using AT2CityLinkAPI.Models;
using AT2CityLinkAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace AT2CityLinkAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingsController : ControllerBase
    {
        private readonly BookingsService _bookingsService;
        public BookingsController(BookingsService bookingService) 
        {
            _bookingsService = bookingService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int skip = 0, [FromQuery] int limit = 10)
        {
            var restaurants = await _bookingsService.GetAllAsync(skip, limit);
            return Ok(restaurants);
        }

        [HttpGet("{id:length(24)}")]
        public async Task<IActionResult> GetById(string id)
        {
            var booking = await _bookingsService.GetByIdAsync(id);
            if (booking == null)
                return NotFound();
            return Ok(booking);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Booking booking)
        {
            await _bookingsService.CreateAsync(booking);
            return CreatedAtAction(nameof(GetById), new { id = booking.Id }, booking);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Booking booking)
        {
            var existing = await _bookingsService.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            booking.Id = id;
            await _bookingsService.UpdateAsync(id, booking);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var booking = await _bookingsService.GetByIdAsync(id);
            if (booking == null)
                return NotFound();

            await _bookingsService.DeleteAsync(id);
            return NoContent();
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> GetByPeopleAmount(string id)
        {
            var booking = await _bookingsService.GetByPeopleAmount(id);
            if (booking == null)
                return NotFound();
            return Ok(booking);
        }


    }
}
