using System.ComponentModel.DataAnnotations;

namespace SCHOOL_MANAGEMENT_API.DTO
{
    public class ClassRoomDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Gradelevel { get; set; }
        public int Capacity { get; set; }
    }

    public class CreateClassRoomDTO
    {
        [Required, MaxLength(50)]
        public string Name { get; set; }
        [Required]
        public int Gradelevel { get; set; }
        [Required]
        public int Capacity { get; set; }
    }

    public class UpdateClassRoomDTO
    {
        [Required, MaxLength(50)]
        public string Name { get; set; }
        [Required]
        public int Gradelevel { get; set; }
        [Required]
        public int Capacity { get; set; }
    }
}
