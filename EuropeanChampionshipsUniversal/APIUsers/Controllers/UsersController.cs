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
    public class UsersController : ApiController
    {
        private europeanchampionshipsdbEntities db = new europeanchampionshipsdbEntities();

        // GET: api/Users
        public IEnumerable<user> Getusers()
        {
            return db.users.ToList();
        }

        // GET: api/Users/5
        [ResponseType(typeof(user))]
        [ActionName("byId")]
        public IHttpActionResult Getuser(int id)
        {
            user user = db.users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        //GET: api/Users/byLogin/?login=
        [ActionName("byLogin")]
        public IEnumerable<user> GetUserByLogin(string login)
        {
            return db.users.Where(u => u.login == login).ToList();
        }

        //Get : api/Users/idUser/favoriteTeamsByIdTeam/idTeam
        [ActionName("favoriteTeamsByIdTeam")]
        [ResponseType(typeof(favoriteteamsuser))]
        public IHttpActionResult GetFavoriteTeamByTeamId(int idUser, int idTeam)
        {
            var user = db.users.Find(idUser);

            if (user == null)
                return NotFound();

            var team = user.favoriteteamsusers.Where(t => t.idTeam == idTeam);
            if (team == null)
                return NotFound();

            return Ok(team);
        }

        // PUT: api/Users/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putuser(int id, user user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.idUser)
            {
                return BadRequest();
            }

            db.Entry(user).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!userExists(id))
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

        // POST: api/Users
        [ResponseType(typeof(user))]
        public IHttpActionResult Postuser(user user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.users.Add(user);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = user.idUser }, user);
        }

        // DELETE: api/Users/5
        [ResponseType(typeof(user))]
        public IHttpActionResult Deleteuser(int id)
        {
            user user = db.users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            db.users.Remove(user);
            db.SaveChanges();

            return Ok(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool userExists(int id)
        {
            return db.users.Count(e => e.idUser == id) > 0;
        }
    }
}