using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SCHOOL_MANAGEMENT_API.Data;
using SCHOOL_MANAGEMENT_API.Models;

namespace SCHOOL_MANAGEMENT_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly AppDbContext _db;

        public StudentController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public ActionResult GetStudents()
        {
            var students = _db.Students.ToList();
            if (students == null || students.Count == 0)
            {
                return NotFound("No students found.");
            }
            return Ok(students);
        }

        [HttpGet("{id:int}")]
        public ActionResult GetStudent(int id)
        {
            var student = _db.Students.FirstOrDefault(s => s.Id == id);
            if (student == null)
            {
                return NotFound("Student not found");
            }
            return Ok(student);
        }

        [HttpGet("{firstname:alpha}")]
        public ActionResult GetStudentByfirstname(string firstname)
        {
            var student = _db.Students
                .Include(s=>s.ClassRoom)
                .FirstOrDefault(s => s.FirstName == firstname);
            if (student == null)
            {
                return NotFound("Student not found");
            }
            return Ok(student);
        }

        [Route("/api/std/{lastname:alpha}")]
        [HttpGet]
        public ActionResult GetStudentByLastname(string lastname)
        {
            var student = _db.Students
                .Include(s => s.ClassRoom)
                .FirstOrDefault(s => s.LastName == lastname);
            if (student == null)
            {
                return NotFound("Student not found");
            }
            return Ok(student);
        }

        [HttpPost]
        public ActionResult CreateStudent(Student student)
        {
            if (student == null || !ModelState.IsValid)
            {
                return BadRequest();
            }
            var classrooms = _db.ClassRooms.Any(s => s.Id == student.ClassRoomId);
            if (classrooms == false)
            {
                return BadRequest("no classroom like that");
            }
            _db.Students.Add(student);
            _db.SaveChanges();
            return Created();
        }

        [HttpPut]
        public ActionResult UpdateStudent(int id, [FromBody] Student student)
        {
            if (id != student.Id)
            {
                return BadRequest();
            }

            var s = _db.Students.FirstOrDefault(s => s.Id == student.Id);
            if (s == null)
            {
                return NotFound();
            }
            else
            {
                s.PhoneNumber = student.PhoneNumber;
                s.LastName = student.LastName;
                s.FirstName = student.FirstName;
                s.ClassRoomId = student.ClassRoomId;
                s.DateofBirth = student.DateofBirth;
                s.Email = student.Email;
                _db.SaveChanges();
            }
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public ActionResult DeleteStudent(int id)
        {
            var s = _db.Students.FirstOrDefault(s => s.Id == id);
            if (s == null)
            {
                return NotFound();
            }
            _db.Remove(s);
            _db.SaveChanges();
            return Ok();
        }


    }
}
