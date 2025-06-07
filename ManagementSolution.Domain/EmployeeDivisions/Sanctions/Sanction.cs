using ManagementSolution.Domain.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations;

namespace ManagementSolution.Domain.EmployeeDivisions.Sanctions
{
    public class Sanction : EmployeeBaseEntity
    {
        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string Punishment { get; set; } = string.Empty;

        [Required]
        public DateTime PunishmentDate { get; set; }

        // Many to One relationship : Many Sanctions to One SanctionType
        public SanctionType SanctionType { get; set; }
        public string SanctionTypeId { get; set; }
    }
}
