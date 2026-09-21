using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SCHOOL_MANAGEMENT_API.Models
{
    public class Teacher
    {
        [Required]
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string FirstName { get; set; }
        [Required, MaxLength(50)]
        public string LastName { get; set; }
        [Required, MaxLength(150),EmailAddress]
        public string Email { get; set; }
        [MaxLength(20),Phone]
        public string PhoneNumber { get; set; }
        [Required,Range(0,maximum:int.MaxValue)]
        public decimal Salary { get; set; }
        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public Department? Department { get; set; }

        public ICollection<Subject> ?Subjects { get; set; }
        
    }
}
