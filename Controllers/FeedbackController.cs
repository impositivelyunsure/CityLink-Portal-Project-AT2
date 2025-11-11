using Microsoft.AspNetCore.Mvc;

public class FeedbackController : Controller
{
    private readonly FeedbackService _feedbackService;

    public FeedbackController(FeedbackService feedbackService)
    {
        _feedbackService = feedbackService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Submit(Feedback feedback)
    {
        if (!ModelState.IsValid)
            return View("Index", feedback);

        await _feedbackService.InsertAsync(feedback);
        return Redirect("/");
    }

}
