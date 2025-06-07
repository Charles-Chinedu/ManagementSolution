using ManagementSolution.Domain.Entities.BaseEntities;
using System.Text.Json.Serialization;

namespace ManagementSolution.Domain.EmployeeDivisions.Sanctions
{
    public class SanctionType : BaseEntity
    {
        [JsonIgnore]
        public List<Sanction>? Sanctions { get; set; }
    }
}
