namespace AgileAPIAT2.Models
{
    public class RegisterDto
    {
        // register info ... to test make sure the password and email get read 
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
