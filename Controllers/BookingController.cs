using Microsoft.AspNetCore.Mvc;


public class BookingController : Controller
{
    private readonly BookingService _bookingService;

    public BookingController(BookingService bookingService)
    {
        _bookingService = bookingService;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
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

    public IActionResult Confirmation()
    {
        return View();
    }
}
