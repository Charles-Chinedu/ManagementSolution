using ManagementSolution.Domain.Entities;
using ManagementSolution.Domain.Entities.BaseEntities;
using System.Text.Json.Serialization;

namespace ManagementSolution.Domain.TertiaryEntities
{
    public class City : BaseEntity
    {
        // Many to One relationship : Many Cities to One Country
        public Country? Country { get; set; }
        public string CountryId { get; set; }

        // One to Many relationship : One City to Many Towns
        [JsonIgnore]
        public List<Town>? Towns { get; set; }
    }
}
