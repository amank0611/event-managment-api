namespace EventManagement.Application.DTOs
{
    public class LoginDto
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
