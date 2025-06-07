using ManagementSolution.Domain.Entities.BaseEntities;
using ManagementSolution.Domain.TertiaryEntities;
using System.Text.Json.Serialization;

namespace ManagementSolution.Domain.Entities
{
    public class Town : BaseEntity
    {
        // Many to One relationship : Many Towns to One City
        public City? City { get; set; }
        public string CityId { get; set; }

        // Relationship : One to Many One Town to Many Employees
        [JsonIgnore]
        public List<Employee>? employees { get; set;}
    }
}
