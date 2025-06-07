using System.ComponentModel.DataAnnotations;

namespace ManagementSolution.Domain.Entities.BaseEntities
{
    public class EmployeeBaseEntity
    {
        [Key]
        public string Id { get; set; }
        public string EmployeeId { get; set; }

        public EmployeeBaseEntity() 
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
