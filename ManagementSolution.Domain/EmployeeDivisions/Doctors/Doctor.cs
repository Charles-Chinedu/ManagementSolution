using ManagementSolution.Domain.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations;

namespace ManagementSolution.Domain.EmployeeDivisions.Doctors
{
    public class Doctor : EmployeeBaseEntity
    {
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string MedicalDiagnose { get; set; } = string.Empty;
        [Required]
        public string MedicalRecommendation { get; set; } = string.Empty;
    }
}
