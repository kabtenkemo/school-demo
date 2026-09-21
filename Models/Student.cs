using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SCHOOL_MANAGEMENT_API.Models
{
    public class Student
    {
        [Required]
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string FirstName { get; set; }
        [Required, MaxLength(50)]
        public string LastName { get; set; }
        [Required, MaxLength(150), EmailAddress]
        public string Email { get; set; }
        [MaxLength(20), Phone]
        public string PhoneNumber { get; set; }
        [Required]
        public DateTime DateofBirth { get; set; }
        [ForeignKey("ClassRoom")]
        public int ClassRoomId { get; set; }
        public ClassRoom? ClassRoom { get; set; }

        public ICollection<Enrollment>? Enrollments { get; set; }
    }
}
