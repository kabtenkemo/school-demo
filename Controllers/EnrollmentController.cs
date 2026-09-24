using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SCHOOL_MANAGEMENT_API.Data;
using SCHOOL_MANAGEMENT_API.DTO;
using SCHOOL_MANAGEMENT_API.Models;

namespace SCHOOL_MANAGEMENT_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;

        public EnrollmentController(AppDbContext db)
        {
            _db = db;

            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.AddProfile<Mapping.EnrollmentProfile>();
            });

            _mapper = config.CreateMapper();
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            var enrollments = _db.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Subject)
                .ToList();

            if (enrollments == null || enrollments.Count == 0)
                return NotFound("No enrollments found.");

            var dto = _mapper.Map<List<EnrollmentDTO>>(enrollments);
            return Ok(dto);
        }

        [HttpGet("{id:int}")]
        public ActionResult Get(int id)
        {
            var enrollment = _db.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Subject)
                .FirstOrDefault(e => e.Id == id);

            if (enrollment == null)
                return NotFound("Enrollment not found.");

            var dto = _mapper.Map<EnrollmentDTO>(enrollment);
            return Ok(dto);
        }

        [HttpPost]
        public ActionResult Create([FromBody] CreateEnrollmentDTO dto)
        {
            if (dto == null || !ModelState.IsValid)
                return BadRequest(ModelState);

            var studentExists = _db.Students.Any(s => s.Id == dto.StudentId);
            var subjectExists = _db.Subjects.Any(s => s.Id == dto.SubjectId);
            if (!studentExists || !subjectExists)
                return BadRequest("Student or Subject not found.");

            var enrollment = _mapper.Map<Enrollment>(dto);
            _db.Enrollments.Add(enrollment);
            _db.SaveChanges();

            var resultDto = _mapper.Map<EnrollmentDTO>(enrollment);
            return CreatedAtAction(nameof(Get), new { id = enrollment.Id }, resultDto);
        }

        [HttpPut("{id:int}")]
        public ActionResult Update(int id, [FromBody] UpdateEnrollmentDTO dto)
        {
            if (dto == null || !ModelState.IsValid)
                return BadRequest(ModelState);

            var enrollment = _db.Enrollments.FirstOrDefault(e => e.Id == id);
            if (enrollment == null)
                return NotFound();

            var studentExists = _db.Students.Any(s => s.Id == dto.StudentId);
            var subjectExists = _db.Subjects.Any(s => s.Id == dto.SubjectId);
            if (!studentExists || !subjectExists)
                return BadRequest("Student or Subject not found.");

            _mapper.Map(dto, enrollment);
            _db.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var enrollment = _db.Enrollments.FirstOrDefault(e => e.Id == id);
            if (enrollment == null)
                return NotFound();

            _db.Enrollments.Remove(enrollment);
            _db.SaveChanges();

            return Ok();
        }

        [HttpGet("last")]
        public ActionResult GetLastEnrollment([FromQuery] int StudentId)
        {
            var enrollment = _db.Enrollments
                .Include(e => e.Student)
                .Last(s=>s.Student.Id==StudentId);
            var dto = _mapper.Map<EnrollmentDTO>(enrollment);
            return Ok(dto);
        }

        [HttpGet("last-or-default")]
        public ActionResult GetLastOrDefaultEnrollment([FromQuery] int StudentId)
        {
            var enrollment = _db.Enrollments
                .Include(e => e.Student)
                .LastOrDefault(s => s.Student.Id == StudentId);
            if(enrollment == null)
                return NotFound("No enrollment found for the given student.");
            var dto = _mapper.Map<EnrollmentDTO>(enrollment);
            return Ok(dto);
        }

        [HttpGet("order-by-classroom-grade")]
        public ActionResult GetEnrollmentsOrderedByGradeDesc()
        { 
            var enrollments = _db.Enrollments
                .OrderBy(e=>e.Student.ClassRoomId)
                .ThenByDescending(e => e.Grade)
                .ToList();
            if (enrollments == null || enrollments.Count == 0)
                return NotFound("No enrollments found.");
            var dto = _mapper.Map<List<EnrollmentDTO>>(enrollments);
            return Ok(dto);
        }

        [HttpGet("avrage-grade")]
        public ActionResult GetAverageGradeByClassroom([FromQuery] int classroomId)
        {
            var average = _db.Enrollments
                .Where(e => e.Student.ClassRoomId == classroomId)
                .Average(e => e.Grade);
            if (average == null)
                return NotFound("No enrollments found for the given classroom.");
            return Ok(new { average });
        }

        [HttpGet("max-grade")]
        public ActionResult GetMaxGradeByClassroom([FromQuery] int classroomId)
        {
            var max = _db.Enrollments
                .Where(e => e.Student.ClassRoomId == classroomId)
                .Max(e => e.Grade);
            if (max == null)
                return NotFound("No enrollments found for the given classroom.");
            return Ok(new { max });
        }

        [HttpGet("min-grade")]
        public ActionResult GetMinGradeByClassroom([FromQuery] int classroomId)
        {
            var min = _db.Enrollments
                .Where(e => e.Student.ClassRoomId == classroomId)
                .Min(e => e.Grade);
            if (min == null)
                return NotFound("No enrollments found for the given classroom.");
            return Ok(new { min });
        }
    }
}
