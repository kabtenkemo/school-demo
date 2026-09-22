using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SCHOOL_MANAGEMENT_API.Data;
using SCHOOL_MANAGEMENT_API.DTO;
using SCHOOL_MANAGEMENT_API.Models;

namespace SCHOOL_MANAGEMENT_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly AppDbContext _db;

        public DepartmentController(AppDbContext context)
        {
            _db = context;


        }

        [HttpGet]
        public IActionResult GetAllDepartment()
        {
            var departments = _db.Departments.ToList();
            if (departments == null || departments.Count == 0)
            {
                return NotFound("Empty");
            }

            var dep = new List<DepartmentDTO>();

            foreach (var department in departments)
            {
                var dptdto = new DepartmentDTO
                {
                    Id = department.Id,
                    Name = department.Name,
                    Description = department.Description
                };
                dep.Add(dptdto);
            }
            return Ok(dep);
        }

        [HttpPost]
        public ActionResult CreateDep(CreateDepDTO department)
        {
            if (department == null)
            {
                return NoContent();
            }

            var d = new Department
            {
                Name = department.Name,
                Description = department.Description
            };

            _db.Departments.Add(d);
            _db.SaveChanges();
            return Created();
        }

        [HttpPut]
        public ActionResult UpdateDepartment(int id, UpdateDepDTO department)
        {
            var existDep = _db.Departments.FirstOrDefault(x => x.Id == id);
            if (existDep == null)
            {
                return NotFound("department not found");
            }

            existDep.Name = department.Name;
            existDep.Description = department.Description;
            _db.SaveChanges();

            return Ok();

        }

        [HttpDelete]
        public ActionResult DeleteDep(int id)
        {
            var existDep = _db.Departments.FirstOrDefault(x => x.Id == id);
            if (existDep == null)
            {
                return NotFound("department not found");
            }
            _db.Departments.Remove(existDep);
            return NoContent();

        }

        [HttpGet("{id}/has-teachers")]
        public ActionResult HasTeachers(int id)
        {
            var department = _db.Departments.FirstOrDefault(d => d.Id == id);
            if (department == null)
            {
                return NotFound("Department not found.");
            }
            bool hasTeachers = _db.Teachers.Any(t => t.DepartmentId == id);
            return Ok(hasTeachers);
        }

        


    }
}
