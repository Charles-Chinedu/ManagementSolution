using ManagementSolution.Domain.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSolution.Domain.EmployeeDivisions.Vacations
{
    public class VacationType : BaseEntity
    {
        public List<Vacation> Vacations { get; set; }
    }
}
