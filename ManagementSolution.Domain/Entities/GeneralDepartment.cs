using ManagementSolution.Domain.Entities.BaseEntities;
using System.Text.Json.Serialization;

namespace ManagementSolution.Domain.Entities
{
    public class GeneralDepartment : BaseEntity
    {
        // one to many relationship with Department
        [JsonIgnore]
        public List<Department>? Departments { get; set; }
    }
}
