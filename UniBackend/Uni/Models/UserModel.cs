using System.ComponentModel.DataAnnotations.Schema;

namespace UniBackend.Models
{
    [Table("User")]
    public class UserModel
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = "User";
        public DateTime RegisteredOn { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;
    }
}
