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
        private readonly AppDbContext Context;

        public DepartmentController(AppDbContext context)
        {
            Context = context;


        }

        [HttpGet]
        public IActionResult GetAllDepartment()
        {
            var departments = Context.Departments.ToList();
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

            Context.Departments.Add(d);
            Context.SaveChanges();
            return Created();
        }

        [HttpPut]
        public ActionResult UpdateDepartment(int id, UpdateDepDTO department)
        {
            var existDep = Context.Departments.FirstOrDefault(x => x.Id == id);
            if (existDep == null)
            {
                return NotFound("department not found");
            }

            existDep.Name = department.Name;
            existDep.Description = department.Description;
            Context.SaveChanges();

            return Ok();

        }

        [HttpDelete]
        public ActionResult DeleteDep(int id)
        {
            var existDep = Context.Departments.FirstOrDefault(x => x.Id == id);
            if (existDep == null)
            {
                return NotFound("department not found");
            }
            Context.Departments.Remove(existDep);
            return NoContent();

        }
    }
}
