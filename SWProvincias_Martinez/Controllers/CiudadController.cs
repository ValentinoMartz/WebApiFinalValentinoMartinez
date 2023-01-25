using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SWProvincias_Martinez.Data;
using SWProvincias_Martinez.Models;
using System.Collections.Generic;
using System.Linq;

namespace SWProvincias_Martinez.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CiudadController : ControllerBase
    {
        private readonly DBPaisFinalContext context;

        public CiudadController(DBPaisFinalContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Ciudad>> Get()
        {
            return context.Ciudades.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Ciudad> GetbyId(short id)
        {
            Ciudad ciudad = (from a in context.Ciudades
                                     where a.IdCiudad == id
                                     select a).SingleOrDefault();

            return ciudad;
        }

        [HttpPost]
        public ActionResult Post(Ciudad ciudad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            context.Ciudades.Add(ciudad);
            context.SaveChanges();
            return Ok();
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Ciudad ciudad)
        {
            if (id != ciudad.IdCiudad)
            {
                return BadRequest();
            }

            context.Entry(ciudad).State = EntityState.Modified;
            context.SaveChanges();

            return Ok();
        }


        [HttpDelete("{id}")]
        public ActionResult<Ciudad> Delete(int id)
        {
            Ciudad ciudad = (from c in context.Ciudades
                             where c.IdCiudad == id
                                   select c).SingleOrDefault();

            if (ciudad == null)
            {
                return NotFound();
            }
            context.Ciudades.Remove(ciudad);
            context.SaveChanges();
            return ciudad;
        }


    }
}
