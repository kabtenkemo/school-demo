using System.ComponentModel.DataAnnotations;

namespace SCHOOL_MANAGEMENT_API.DTO
{
    public class DepartmentDTO
    {
        public int Id { get; set; }

        public string? Name { get; set; }
        public string? Description { get; set; }
    }

    public class CreateDepDTO
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
    }

    public class UpdateDepDTO
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
