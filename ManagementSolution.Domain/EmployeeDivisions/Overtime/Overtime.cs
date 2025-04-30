using ManagementSolution.Domain.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations;

namespace ManagementSolution.Domain.EmployeeDivisions.Overtime
{
    public class Overtime : EmployeeBaseEntity
    {
        [Required]
        public DateTime StartDate { get; set; }
        
        [Required]
        public DateTime EndDate { get; set; }

        public string Duration 
        {
            get
            {
                TimeSpan span = EndDate - StartDate;
                int days = span.Days;
                int hours = span.Hours;

                return $"{days} day{(days != 1 ? "s" : "")}, {hours} hour{(hours != 1 ? "s" : "")}";
            }
        }
        // Many to One relationship : Many Overtime to One OvertimeType
        public OvertimeType? OverTimeType { get; set; }

        [Required]
        public int OvertimeTypeId { get; set; }
    }
}
