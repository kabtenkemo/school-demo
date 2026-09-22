using System.ComponentModel.DataAnnotations;

namespace SCHOOL_MANAGEMENT_API.DTO
{
    public class EnrollmentDTO
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public decimal Grade { get; set; }
    }

    public class CreateEnrollmentDTO
    {
        [Required]
        public int StudentId { get; set; }

        [Required]
        public int SubjectId { get; set; }

        public DateTime EnrollmentDate { get; set; } = DateTime.UtcNow;

        [Range(0, 100)]
        public decimal Grade { get; set; }
    }

    public class UpdateEnrollmentDTO
    {
        [Required]
        public int StudentId { get; set; }

        [Required]
        public int SubjectId { get; set; }

        public DateTime EnrollmentDate { get; set; }

        [Range(0, 100)]
        public decimal Grade { get; set; }
    }
}
