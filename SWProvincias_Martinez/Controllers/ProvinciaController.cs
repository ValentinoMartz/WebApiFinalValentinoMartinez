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
    public class ProvinciaController : ControllerBase
    {
        private readonly DBPaisFinalContext context;

        public ProvinciaController(DBPaisFinalContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Provincia>> Get()
        {
            return context.Provincias.ToList();
        }

        [HttpPost]
        public ActionResult Post(Provincia provincia)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            context.Provincias.Add(provincia);
            context.SaveChanges();
            return Ok();
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Provincia provincia)
        {
            if (id != provincia.IdProvincia)
            {
                return BadRequest();
            }

            context.Entry(provincia).State = EntityState.Modified;
            context.SaveChanges();

            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult<Provincia> Delete(int id)
        {
            Provincia provincia = (from p in context.Provincias
                                   where p.IdProvincia == id
                                   select p).SingleOrDefault();

            if (provincia == null)
            {
                return NotFound();
            }
            context.Provincias.Remove(provincia);
            context.SaveChanges();
            return provincia;
        }
    }
}
