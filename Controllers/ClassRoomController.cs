using AutoMapper;
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
    public class ClassRoomController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;

        public ClassRoomController(AppDbContext db)
        {
            _db = db;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ClassRoomProfile>();
                cfg.AddProfile<StudentProfile>();
            });

            _mapper = config.CreateMapper();
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            var rooms = _db.ClassRooms.ToList();
            if (rooms == null || rooms.Count == 0)
                return NotFound("No classrooms found.");

            var dto = _mapper.Map<List<ClassRoomDTO>>(rooms);
            return Ok(dto);
        }

        [HttpGet("{id:int}")]
        public ActionResult Get(int id)
        {
            var room = _db.ClassRooms.FirstOrDefault(r => r.Id == id);
            if (room == null)
                return NotFound("Classroom not found.");

            var dto = _mapper.Map<ClassRoomDTO>(room);
            return Ok(dto);
        }

        [HttpPost]
        public ActionResult Create([FromBody] CreateClassRoomDTO dto)
        {
            if (dto == null || !ModelState.IsValid)
                return BadRequest(ModelState);

            var room = _mapper.Map<ClassRoom>(dto);
            _db.ClassRooms.Add(room);
            _db.SaveChanges();

            return CreatedAtAction(nameof(Get), new { id = room.Id }, null);
        }

        [HttpPut("{id:int}")]
        public ActionResult Update(int id, [FromBody] UpdateClassRoomDTO dto)
        {
            if (dto == null || !ModelState.IsValid)
                return BadRequest(ModelState);

            var room = _db.ClassRooms.FirstOrDefault(r => r.Id == id);
            if (room == null)
                return NotFound();

            _mapper.Map(dto, room);
            _db.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var room = _db.ClassRooms.FirstOrDefault(r => r.Id == id);
            if (room == null)
                return NotFound();

            _db.ClassRooms.Remove(room);
            _db.SaveChanges();
            return Ok();
        }

        [HttpGet("{id}/all-have-phones")]
        public ActionResult GetAllStudentsWithPhones(int id)
        {
            var classroom = _db.ClassRooms
                .Include(r => r.Students)
                .FirstOrDefault(r => r.Id == id);

            if (classroom == null)
                return NotFound("Classroom not found.");

            if (classroom.Students == null || classroom.Students.Count == 0)
                return NotFound("No students found in this classroom.");

            var allHavePhones = classroom.Students.All(s => !string.IsNullOrEmpty(s.PhoneNumber));
            

            return Ok(allHavePhones);
        }
    }
}
