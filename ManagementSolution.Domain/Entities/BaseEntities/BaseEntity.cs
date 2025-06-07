using System.ComponentModel.DataAnnotations;

namespace ManagementSolution.Domain.Entities.BaseEntities
{
    public class BaseEntity
    {
        [Key]
        public string Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        
        public BaseEntity()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
