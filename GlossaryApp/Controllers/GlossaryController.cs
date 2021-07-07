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
using GlossaryApp;

namespace GlossaryApp.Controllers
{
    public class GlossaryController : ApiController
    {
        private DBModel db = new DBModel();

        // GET: api/Glossary
        public IQueryable<Glossary> GetGlossaries()
        {
            return db.Glossaries.OrderBy(alphabetically => alphabetically.Term);
        }

        // GET: api/Glossary/5
        [ResponseType(typeof(Glossary))]
        public IHttpActionResult GetGlossary(int id)
        {
            Glossary glossary = db.Glossaries.Find(id);
            if (glossary == null)
            {
                return NotFound();
            }

            return Ok(glossary);
        }

        // PUT: api/Glossary/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutGlossary(int id, Glossary glossary)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != glossary.TermID)
            {
                return BadRequest();
            }

            db.Entry(glossary).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GlossaryExists(id))
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

        // POST: api/Glossary
        [ResponseType(typeof(Glossary))]
        public IHttpActionResult PostGlossary(Glossary glossary)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Glossaries.Add(glossary);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = glossary.TermID }, glossary);
        }

        // DELETE: api/Glossary/5
        [ResponseType(typeof(Glossary))]
        public IHttpActionResult DeleteGlossary(int id)
        {
            Glossary glossary = db.Glossaries.Find(id);
            if (glossary == null)
            {
                return NotFound();
            }

            db.Glossaries.Remove(glossary);
            db.SaveChanges();

            return Ok(glossary);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GlossaryExists(int id)
        {
            return db.Glossaries.Count(e => e.TermID == id) > 0;
        }
    }
}