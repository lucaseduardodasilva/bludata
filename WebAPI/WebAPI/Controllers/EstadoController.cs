using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class EstadoController : ApiController
    {
        private DBModel db = new DBModel();

        // GET: api/Estado
        //public IQueryable<Estado> GetEstado()
        //{
        //    return db.Estado;
        //}

        // GET: api/Estado/5
        [Route("api/GetEstado/")]
        [HttpGet]
        [ResponseType(typeof(Estado))]
        public string GetEstado()
        {
            return JsonConvert.SerializeObject(db.Estado.ToList());
        }

            //Estado estado = db.Estado.Find(id);
            //if (estado == null)
            //{
            //    return NotFound();
            //}

            //return Ok(estado);

        // PUT: api/Estado/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEstado(int id, Estado estado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != estado.EstadoId)
            {
                return BadRequest();
            }

            db.Entry(estado).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EstadoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Estado
        [ResponseType(typeof(Estado))]
        public IHttpActionResult PostEstado(Estado estado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Estado.Add(estado);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = estado.EstadoId }, estado);
        }

        // DELETE: api/Estado/5
        [ResponseType(typeof(Estado))]
        public IHttpActionResult DeleteEstado(int id)
        {
            Estado estado = db.Estado.Find(id);
            if (estado == null)
            {
                return NotFound();
            }

            db.Estado.Remove(estado);
            db.SaveChanges();

            return Ok(estado);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EstadoExists(int id)
        {
            return db.Estado.Count(e => e.EstadoId == id) > 0;
        }
    }
}