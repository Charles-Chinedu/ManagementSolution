using ManagementSolution.Domain.Entities.BaseEntities;

namespace ManagementSolution.Domain.Entities
{
    public class Employee : BaseEntity
    {
        public string? CivilId { get; set; }

        public string? FileNumber { get; set; }

        public string? FullName { get; set; }

        public string? JobName { get; set; }

        public string? Address { get; set; }

        public string? TelephoneNumber { get; set; }

        public string? Photo { get; set; }

        public string? Other { get; set; }

        public GeneralDepartment? GeneralDepartment { get; set; }
        public string GeneralDepartmentId { get; set; }

        public Department? Department { get; set; }
        public string DepartmentId { get; set; }

        public Branch? Branch { get; set; }
        public string BranchId { get; set; }

        public Town? Town { get; set; }
        public string TownId { get; set; }
    }
}
