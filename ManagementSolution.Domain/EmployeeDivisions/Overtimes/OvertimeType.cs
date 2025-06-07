using ManagementSolution.Domain.Entities.BaseEntities;
using System.Text.Json.Serialization;

namespace ManagementSolution.Domain.EmployeeDivisions.Overtimes
{
    public class OvertimeType : BaseEntity
    {
        [JsonIgnore]
        public List<Overtime>? Overtimes { get; set; }
    }
}
