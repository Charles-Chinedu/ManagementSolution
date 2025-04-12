using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ManagementSolution.Domain.Entities.BaseEntities
{
    public class BaseEntity
    {
        [Key]
        public string Id { get; set; }
        public string? Name { get; set; }

        // Relationship: One to Many
        [JsonIgnore]
        public List<Employee>? Employees { get; set; }

        public BaseEntity()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
