using System.ComponentModel.DataAnnotations;

namespace SCHOOL_MANAGEMENT_API.DTO
{
    public class CreateTeacherDTO
    {
        [Required, MaxLength(50)]
        public string FirstName { get; set; }
        [Required, MaxLength(50)]
        public string LastName { get; set; }
        [Required, MaxLength(150), EmailAddress]
        public string Email { get; set; }
        [MaxLength(20), Phone]
        public string PhoneNumber { get; set; }
        [Required, Range(0, int.MaxValue)]
        public decimal Salary { get; set; }
        [Required]
        public int DepartmentId { get; set; }
    }
}
