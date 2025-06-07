using System.ComponentModel.DataAnnotations;

namespace ManagementSolution.Application.DTOs.EmployeeGrouping
{
    public class EmployeeGroupinging2
    {
        [Required]
        public string JobName { get; set; } = string.Empty;

        [Required, Range(1, 99999, ErrorMessage = "You must select branch")]
        public string BranchId { get; set; }

        [Required, Range(1, 99999, ErrorMessage = "You must select town")]
        public string TownId { get; set; }

        [Required]
        public string? Other { get; set; }
    }
}
