using ManagementSolution.Domain.Entities.BaseEntities;

namespace ManagementSolution.Domain.Entities
{
    public class GeneralDepartment : BaseEntity
    {
        // one to many relationship with Department
        public List<Department>? Departments { get; set; }
    }
}
