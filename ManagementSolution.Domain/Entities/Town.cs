using ManagementSolution.Domain.Entities.BaseEntities;
using ManagementSolution.Domain.TertiaryEntities;

namespace ManagementSolution.Domain.Entities
{
    public class Town : BaseEntity
    {
        // Relationship : One to Many One Town to Many Employees
        public List<Employee> employees { get; set;}

        // Many to One relationship : Many Towns to One City
        public City? City { get; set; }
        public string CityId { get; set; }
    }
}
