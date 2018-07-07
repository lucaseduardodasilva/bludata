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
using System.Web.Http.Cors;
using Newtonsoft.Json;

namespace WebAPI.Controllers
{
    //[EnableCors(origins: "*", headers: "*", methods: "*")]

    public class ClienteController : ApiController
    {
        private DBModel db = new DBModel();

        // GET: api/Cliente
        [Route("api/GetCliente/")]
        [HttpGet]
        public string GetCliente()
        {
            return JsonConvert.SerializeObject(db.Cliente.ToList());
        }

        [HttpPut]
        [Route("api/PutCliente/{id}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCliente(int id, Cliente cliente)
        {
            if (id != cliente.ClienteID)
            {
                return BadRequest();
            }

            db.Entry(cliente).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExists(id))
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

        [Route("api/PostCliente/")]
        [HttpPost]
        [ResponseType(typeof(Cliente))]
        public IHttpActionResult PostCliente(Cliente request)
        {

            try
            {
                if (!VerificaCpfDuplicado(request.Cpf))
                {
                    db.Cliente.Add(request);

                    db.SaveChanges();
                }
                else
                {
                    return Conflict();
                }

            }
            catch (DbUpdateException ex)
            {
                return InternalServerError(ex);
            }

            return Ok();
        }

        [HttpDelete]
        [Route("api/DeleteCliente/{id}")]
        [ResponseType(typeof(Cliente))]
        public IHttpActionResult DeleteCliente(int id)
        {
            Cliente cliente = db.Cliente.Find(id);
            if (cliente == null)
            {
                return NotFound();
            }

            db.Cliente.Remove(cliente);
            db.SaveChanges();

            return Ok(cliente);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ClienteExists(int id)
        {
            return db.Cliente.Count(e => e.ClienteID == id) > 0;
        }

        private bool VerificaCpfDuplicado(string Cpf)
        {
            return db.Cliente.Count(c => c.Cpf == Cpf) > 0;
        }
    }
}