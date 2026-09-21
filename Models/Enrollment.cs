using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SCHOOL_MANAGEMENT_API.Models
{
    public class Enrollment
    {
        [Required]
        public int Id { get; set; }
        [ForeignKey("Student")]
        public int StudentId { get; set; }
        [ForeignKey("Subject")]
        public int SubjectId { get; set; }
        [Required]
        public DateTime EnrollmentDate { get; set; } = DateTime.UtcNow;
        [Range(0, 100)]
        public decimal Grade { get; set; } 
        public Student ?Student { get; set; }
        public Subject ?Subject { get; set; }
    }
}
