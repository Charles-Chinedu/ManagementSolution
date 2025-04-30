using ManagementSolution.Domain.Entities.BaseEntities;

namespace ManagementSolution.Domain.Entities
{
    public class Branch : BaseEntity
    {
        // Many to one relationship : Many Branches to One Department
        public Department? Department { get; set; }

        public string DepartmentId { get; set; }

        // Relationship : One to Many : One Branch Many Employees
        public List<Employee>? Employees { get; set; }
    }
}
