using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SCHOOL_MANAGEMENT_API.DTO
{
    public class StudentDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateofBirth { get; set; }
        public int ClassRoomId { get; set; }
    }

    public class CreateStudentDTO
    {
        public string FullName { get; set; }

        public string Email { get; set; }
        [MaxLength(20), Phone]
        public string PhoneNumber { get; set; }
        [Required]
        public DateTime DateofBirth { get; set; }
        [Required]
        public int ClassRoomId { get; set; }
    }

    public class UpdateStudentDTO
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        [MaxLength(20), Phone]
        public string PhoneNumber { get; set; }
        
        public DateTime DateofBirth { get; set; }
        
        public int ClassRoomId { get; set; }
    }
}
