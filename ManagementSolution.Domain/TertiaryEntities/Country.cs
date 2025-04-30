using ManagementSolution.Domain.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSolution.Domain.TertiaryEntities
{
    public class Country : BaseEntity
    {
        // One to Many relationship One Country to Many Cities
        public List<City>? Cities { get; set; }
    }
}
