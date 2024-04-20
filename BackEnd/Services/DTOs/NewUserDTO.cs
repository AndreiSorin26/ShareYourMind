namespace Services.DTOs
{
    public class NewUserDTO
    {
        public String Username { get; set; } = default!;
        public String Password { get; set; } = default!;
        public String Role { get; set; } = default!;
    }
}
