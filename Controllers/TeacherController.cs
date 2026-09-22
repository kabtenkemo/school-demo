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

        [HttpGet("{id:int}")]
        public ActionResult GetTeacher(int id)
        {
            var item = _db.Teachers
                .Include(t => t.Department)
                .FirstOrDefault(t => t.Id == id);

            if (item == null)
                return NotFound("Teacher not found.");

            var dto = new TeacherDto
            {
                FirstName = item.FirstName,
                LastName = item.LastName,
                Email = item.Email,
                PhoneNumber = item.PhoneNumber,
                Salary = item.Salary,
                DepartmentId = item.DepartmentId,
                DepartmentName = item.Department?.Name
            };

            return Ok(dto);
        }

        [HttpPost]
        public ActionResult CreateTeacher([FromBody] CreateTeacherDTO dto)
        {
            if (dto == null || !ModelState.IsValid)
                return BadRequest(ModelState);

            var deptExists = _db.Departments.Any(d => d.Id == dto.DepartmentId);
            if (!deptExists)
                return BadRequest("Department not found.");

            var teacher = new Models.Teacher
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                Salary = dto.Salary,
                DepartmentId = dto.DepartmentId
            };

            _db.Teachers.Add(teacher);
            _db.SaveChanges();

            return CreatedAtAction(nameof(GetTeacher), new { id = teacher.Id }, null);
        }

        [HttpPut("{id:int}")]
        public ActionResult UpdateTeacher(int id, [FromBody] UpdateTeacherDTO dto)
        {
            if (dto == null || !ModelState.IsValid)
                return BadRequest(ModelState);

            var teacher = _db.Teachers.FirstOrDefault(t => t.Id == id);
            if (teacher == null)
                return NotFound();

            var deptExists = _db.Departments.Any(d => d.Id == dto.DepartmentId);
            if (!deptExists)
                return BadRequest("Department not found.");

            teacher.FirstName = dto.FirstName;
            teacher.LastName = dto.LastName;
            teacher.Email = dto.Email;
            teacher.PhoneNumber = dto.PhoneNumber;
            teacher.Salary = dto.Salary;
            teacher.DepartmentId = dto.DepartmentId;

            _db.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public ActionResult DeleteTeacher(int id)
        {
            var teacher = _db.Teachers.FirstOrDefault(t => t.Id == id);
            if (teacher == null)
                return NotFound();

            _db.Teachers.Remove(teacher);
            _db.SaveChanges();

            return Ok();
        }


    }
}
