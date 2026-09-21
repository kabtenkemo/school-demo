using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace SCHOOL_MANAGEMENT_API.Models
{
    public class ClassRoom
    {
        [Required]
        public int Id { get; set; }
        [Required, MaxLength(50),NotNull]
        public string Name { get; set; }
        [Required,Range(1, 12)]
        public int Gradelevel { get; set; }
        [Required,Range(1, 100)]
        public int Capacity { get; set; }
        [JsonIgnore]
        public ICollection<Student>? Students { get; set; }
    }
}
