using System.ComponentModel.DataAnnotations;

namespace ManagementSolution.Infrastructure.Models
{
    public class ApplicationUser
    {
        [Key]
        public string Id { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string Image { get; set; } = string.Empty;
    }
}
