using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace SCHOOL_MANAGEMENT_API.Models
{
    public class Department
    {
        [Required]
        public int Id { get; set; }
        [Required,MaxLength(100)]
        public string ?Name { get; set; }
        [MaxLength(500)]
        public string ?Description { get; set; }
        public ICollection<Teacher>? Teachers { get; set; }
    }
}
