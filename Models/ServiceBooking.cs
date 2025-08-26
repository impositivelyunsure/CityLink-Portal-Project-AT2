using System.ComponentModel.DataAnnotations;

public class ServiceBooking
{
    [Required, Display(Name = "Full Name")]
    public string FullName { get; set; } = string.Empty;

    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required, Display(Name = "Service Type")]
    public string ServiceType { get; set; } = string.Empty;

    [Required, DataType(DataType.Date), Display(Name = "Preferred Date")]
    public DateTime? PreferredDate { get; set; }
}
