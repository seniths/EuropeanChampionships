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
using APIUsers.Models;

namespace APIUsers.Controllers
{
    public class FavoriteteamsusersController : ApiController
    {
        private europeanchampionshipsdbEntities db = new europeanchampionshipsdbEntities();

        // GET: api/Favoriteteamsusers
        public IEnumerable<favoriteteamsuser> Getfavoriteteamsusers()
        {
            return db.favoriteteamsusers.ToList();
        }

        // GET: api/Favoriteteamsusers/5
        [ResponseType(typeof(favoriteteamsuser))]
        [ActionName("byId")]
        public IHttpActionResult Getfavoriteteamsuser(int id)
        {
            favoriteteamsuser favoriteteamsuser = db.favoriteteamsusers.Find(id);
            if (favoriteteamsuser == null)
            {
                return NotFound();
            }

            return Ok(favoriteteamsuser);
        }

        // PUT: api/Favoriteteamsusers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putfavoriteteamsuser(int id, favoriteteamsuser favoriteteamsuser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != favoriteteamsuser.idFavoriteTeamsUser)
            {
                return BadRequest();
            }

            db.Entry(favoriteteamsuser).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!favoriteteamsuserExists(id))
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

        // POST: api/Favoriteteamsusers
        [ResponseType(typeof(favoriteteamsuser))]
        public IHttpActionResult Postfavoriteteamsuser(favoriteteamsuser favoriteteamsuser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.favoriteteamsusers.Add(favoriteteamsuser);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = favoriteteamsuser.idFavoriteTeamsUser }, favoriteteamsuser);
        }

        // DELETE: api/Favoriteteamsusers/5
        [ResponseType(typeof(favoriteteamsuser))]
        [ActionName("DeleteById")]
        public IHttpActionResult Deletefavoriteteamsuser(int id)
        {
            favoriteteamsuser favoriteteamsuser = db.favoriteteamsusers.Find(id);
            if (favoriteteamsuser == null)
            {
                return NotFound();
            }

            db.favoriteteamsusers.Remove(favoriteteamsuser);
            db.SaveChanges();

            return Ok(favoriteteamsuser);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool favoriteteamsuserExists(int id)
        {
            return db.favoriteteamsusers.Count(e => e.idFavoriteTeamsUser == id) > 0;
        }
    }
}