public class AdminDashboardViewModel
{
    public IEnumerable<User> Users { get; set; } = new List<User>();
    public IEnumerable<Announcement> Announcements { get; set; } = new List<Announcement>();
    public IEnumerable<Feedback> Feedback { get; set; } = new List<Feedback>();
    public IEnumerable<Booking> Bookings { get; set; } = new List<Booking>();
    public int CurrentPage { get; set; } = 1;
    public int TotalPages { get; set; } = 1;
    public int Limit { get; set; } = 20;
}
