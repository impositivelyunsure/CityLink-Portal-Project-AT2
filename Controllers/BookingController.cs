using Microsoft.AspNetCore.Mvc;

[Route("bookings")]
public class BookingController : Controller
{
    private readonly BookingService _bookingService;

    public BookingController(BookingService bookingService)
    {
        _bookingService = bookingService;
    }

    [HttpGet("")]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost("book")]
    public async Task<IActionResult> Book(string serviceType, DateTime date, int size)
    {
        await _bookingService.CreateAsync(serviceType, size, date);
        var booking = new Booking
        {
            BookingId = serviceType,
            Size = size,
            Date = date
        };
        return View("Confirmation", booking);
    }

    [HttpGet("confirmation")]
    public IActionResult Confirmation()
    {
        return View();
    }
}
