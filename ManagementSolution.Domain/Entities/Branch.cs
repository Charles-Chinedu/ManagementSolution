using ManagementSolution.Domain.Entities.BaseEntities;
using System.Text.Json.Serialization;

namespace ManagementSolution.Domain.Entities
{
    public class Branch : BaseEntity
    {
        // Many to one relationship : Many Branches to One Department
        public Department? Department { get; set; }

        public string DepartmentId { get; set; }

        // Relationship : One to Many : One Branch Many Employees
        [JsonIgnore]
        public List<Employee>? Employees { get; set; }
    }
}
