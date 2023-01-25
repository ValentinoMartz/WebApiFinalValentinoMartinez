using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using SWAdventureWorks_Martinez.Models;
using SWAdventureWorks_Martinez.Controllers;

namespace SWAdventureWorks_Martinez.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly AdventureWorks2019Context context;

        public DepartmentController(AdventureWorks2019Context context)
        {
            this.context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Department>> Get()
        {
            return context.Department.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Department> GetbyId(short id)
        {
            Department department = (from a in context.Department
                                  where a.DepartmentId == id
                               select a).SingleOrDefault();

            return department;
        }

        [HttpGet("name/{name}")]
        public ActionResult<Department> GetbyName(string name)
        {
            Department department = (from a in context.Department
                                     where a.Name == name
                                     select a).SingleOrDefault();

            return department;
        }

        [HttpPost]
        public ActionResult Post(Department department)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            context.Department.Add(department);
            context.SaveChanges();
            return Ok();
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Department department)
        {
            if (id != department.DepartmentId)
            {
                return BadRequest();
            }

            context.Entry(department).State = EntityState.Modified;
            context.SaveChanges();

            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult<Department> Delete(short id)
        {
            var department = (from c in context.Department
                              where c.DepartmentId == id
                              select c).SingleOrDefault();

            if (department == null)
            {
                return NotFound();
            }
            context.Department.Remove(department);
            context.SaveChanges();
            return department;
        }
    }

}
