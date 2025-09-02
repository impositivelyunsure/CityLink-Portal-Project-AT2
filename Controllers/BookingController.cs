using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("api/[controller]")]

// Booking controller
public class BookingController : ControllerBase
{
    private readonly BookingService _bookingService;

    public BookingController(BookingService bookingService)
    {
        _bookingService = bookingService;
    }

    // Get method for retrieving all (hard coded limit of 10) bookings.
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int skip = 0, [FromQuery] int limit = 10)
    {
        var booking = await _bookingService.GetAllAsync(skip, limit);
        return Ok(booking);
    }

    // Get method for retrieving a booking by its booking ID.
    // Requires a booking ID.
    [HttpGet("{booking_Id}")]
    public async Task<IActionResult> GetByBookingId(string bookingId, [FromQuery] int limit = 24)
    {
        // return the results based upon the given ID, and the limit.
        var results = await _bookingService.GetByBookingIdAsync(bookingId, limit);
        return results.Any() ? Ok(results) : NotFound();
    }


    // Get method for retrieving a booking by its MongoDB ID.
    // Requires an ID.
    [HttpGet("{mongoDB_Id}")]
    public async Task<IActionResult> GetById(string id)
    {
        // return the result based upon a given ID.
        var booking = await _bookingService.GetByIdAsync(id);
        if (booking == null)
            return NotFound();
        return Ok(booking);
    }

    // Get method for retrieving a booking by its size.
    // Requires a size.
    [HttpGet("{size}")]
    public async Task<IActionResult> GetBySize(int size)
    {
        // return the result based upon a given size.
        var booking = await _bookingService.GetBySizeAsync(size);
        if (booking == null)
            return NotFound();
        return Ok(booking);
    }

    // Post method for posting a booking.
    // Requires a booking ID, size, and date.
    [HttpPost]
    public async Task<IActionResult> Create(string bookingId, int size, DateTime date)
    {
        // return the result based upon a given ID, size, and date.
        await _bookingService.CreateAsync(bookingId, size, date);
        return Ok();
    }


    // Put method for updating a record's size and date, based on the bookings MongoDB ID.
    // Requires the MongoDB ID, size, and date.
    [HttpPut("{id:length(24)} {size} {date}")] // Anytime you see ID:length 24, it refers to MongoDBs inbuilt object id that it gives to all database records.
    public async Task<IActionResult> Update(string id, int size, DateTime date)
    {
        var existing = await _bookingService.GetByIdAsync(id);
        if (existing == null)
            return NotFound();

        await _bookingService.UpdateAsync(id, size, date);
        return NoContent();
    }

    // Delete method for deleting a record based on the MongoDB ID.
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
