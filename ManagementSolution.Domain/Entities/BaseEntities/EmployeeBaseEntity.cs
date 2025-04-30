using System.ComponentModel.DataAnnotations;

namespace ManagementSolution.Domain.Entities.BaseEntities
{
    public class EmployeeBaseEntity
    {
        [Key]
        public string Id { get; set; }
        [Required]
        public string CivilId { get; set; } = string.Empty;
        [Required]
        public string FileNumber { get; set; } = string.Empty;
        public string? Other { get; set; }
    }
}
