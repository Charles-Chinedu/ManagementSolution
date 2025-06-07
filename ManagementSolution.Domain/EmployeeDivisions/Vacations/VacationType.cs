using ManagementSolution.Domain.Entities.BaseEntities;
using System.Text.Json.Serialization;

namespace ManagementSolution.Domain.EmployeeDivisions.Vacations
{
    public class VacationType : BaseEntity
    {
        // One to Many relationship : One VacationType to Many Vacations
        [JsonIgnore]
        public List<Vacation>? Vacations { get; set; }
    }
}
