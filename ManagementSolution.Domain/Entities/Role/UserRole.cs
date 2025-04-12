using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSolution.Domain.Entities.Role
{
    public class UserRole
    {
        public string Id { get; set; }

        public string RoleId { get; set; }

        public string UserId { get; set; }
    }
}
