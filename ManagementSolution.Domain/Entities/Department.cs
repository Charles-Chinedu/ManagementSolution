using ManagementSolution.Domain.Entities.BaseEntities;

namespace ManagementSolution.Domain.Entities
{
    public class Department : BaseEntity
    {
        // Many to one relationship Many Departments to One GeneralDepartment
        public GeneralDepartment? GeneralDepartment { get; set; }

        public string GeneralDepartmentId { get; set; }

        // one to many relationship : One Department to Many Branches
        public List<Branch>? Branches { get; set; }
    }
}
