using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SCHOOL_MANAGEMENT_API.Data;
using Microsoft.EntityFrameworkCore;
using SCHOOL_MANAGEMENT_API.DTO;

namespace SCHOOL_MANAGEMENT_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly AppDbContext _db;
        public TeacherController(AppDbContext db)
        {
            _db=db;
        }

        [HttpGet]
        public ActionResult GetAllTeachers()
        {
            var teachers = _db.Teachers
                .Include(t => t.Department)
                .ToList();
            if (teachers == null || teachers.Count == 0)
            { 
            return NotFound("No teachers found.");
            }

            var teacherDTOs = new List<TeacherDto>();
            foreach (var item in teachers)
            {
                var teacherDTO = new TeacherDto
                {
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    Email = item.Email,
                    PhoneNumber = item.PhoneNumber,
                    Salary = item.Salary,
                    DepartmentId = item.DepartmentId,
                    DepartmentName = item.Department?.Name
                };
                teacherDTOs.Add(teacherDTO);
            }
            return Ok(teacherDTOs);
        }
    }
}
