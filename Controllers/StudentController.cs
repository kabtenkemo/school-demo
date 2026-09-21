using AutoMapper;
using AutoMapper.Configuration.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SCHOOL_MANAGEMENT_API.Data;
using SCHOOL_MANAGEMENT_API.DTO;
using SCHOOL_MANAGEMENT_API.Mapping;
using SCHOOL_MANAGEMENT_API.Models;

namespace SCHOOL_MANAGEMENT_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;
        public StudentController(AppDbContext db)
        {
            _db = db;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<StudentProfile>();
            });

            _mapper = config.CreateMapper();
        }

        [HttpGet]
        public ActionResult GetStudents()
        {
            var students = _db.Students.ToList();
            if (students == null || students.Count == 0)
            {
                return NotFound("No students found.");
            }
            var dto=_mapper.Map<List<StudentDTO>>(students);
            return Ok(dto);
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
        public ActionResult CreateStudent([FromBody] DTO.CreateStudentDTO dto)
        {
            if (dto == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var classroomExists = _db.ClassRooms.Any(c => c.Id == dto.ClassRoomId);
            if (!classroomExists)
            {
                return BadRequest("No classroom like that");
            }

            var student = _mapper.Map<Student>(dto);
            _db.Students.Add(student);
            _db.SaveChanges();

            var resultDto = _mapper.Map<DTO.StudentDTO>(student);
            return Created();
        }

        [HttpPut("{id:int}")]
        public ActionResult UpdateStudent(int id, [FromBody] DTO.UpdateStudentDTO dto)
        {
            if (dto == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var student = _db.Students.FirstOrDefault(s => s.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            // map dto into existing entity
            _mapper.Map(dto, student);

            var classroomExists = _db.ClassRooms.Any(c => c.Id == dto.ClassRoomId);
            if (!classroomExists)
            {
                return BadRequest("No classroom like that");
            }

            _db.SaveChanges();
            return NoContent();
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
