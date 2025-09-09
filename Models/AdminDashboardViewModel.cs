public class AdminDashboardViewModel
{
    public IEnumerable<User> Users { get; set; } = new List<User>();
    public IEnumerable<Announcement> Announcements { get; set; } = new List<Announcement>();
    public IEnumerable<Feedback> Feedback { get; set; } = new List<Feedback>();
}
