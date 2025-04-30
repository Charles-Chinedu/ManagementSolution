using ManagementSolution.Domain.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSolution.Domain.EmployeeDivisions.Sanction
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
