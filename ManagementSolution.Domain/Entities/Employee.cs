using ManagementSolution.Domain.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations;

namespace ManagementSolution.Domain.Entities
{
    public class Employee : BaseEntity
    {
        [Required]
        public string? CivilId { get; set; }
        [Required]
        public string? FileNumber { get; set; }
        [Required]
        public string? FullName { get; set; }
        [Required]
        public string? JobName { get; set; }
        [Required]
        public string? Address { get; set; }
        [Required, DataType(DataType.PhoneNumber]
        public string? TelephoneNumber { get; set; }
        [Required]
        public string? Photo { get; set; }

        public string? Other { get; set; }

        // Many to one relationship : Many Employees to One Branch
        public Branch? Branch { get; set; }
        public string BranchId { get; set; }

        // Many to one relationship : Many Employees to One Town 
        public Town? Town { get; set; }
        public string TownId { get; set; }
    }
}
