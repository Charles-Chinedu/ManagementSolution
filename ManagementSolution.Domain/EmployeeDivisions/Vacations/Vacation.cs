using ManagementSolution.Domain.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations;

namespace ManagementSolution.Domain.EmployeeDivisions.Vacations
{
    public class Vacation : EmployeeBaseEntity
    {
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public int NumberOfDays { get; set; }
        public DateTime EndDate { get; set; }

        // Many to One relationship : Many Vacations to One VacationType
        public VacationType VacationType { get; set; }

        [Required]
        public string VacationTypeId { get; set; }
    }
}
