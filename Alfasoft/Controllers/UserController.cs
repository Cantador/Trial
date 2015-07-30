using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Alfasoft.Models;

namespace Alfasoft.Controllers
{
    public class UserController : ApiController
    {
        private AppContext db = new AppContext();

        // GET api/Users
        [HttpGet]
        [ActionName("GetUsers")]
        public IEnumerable<User> GetUsers()
        {
            return db.User.AsEnumerable();
        }

        // GET api/Users/5
        [HttpGet]
        [ActionName("GetUser")]
        public User GetUser(int id)
        {
            User users = db.User.Find(id);
            if (users == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return users;
        }

        // PUT api/Users/5
        [HttpPut]
        public HttpResponseMessage PutUser(int id, User users)
        {
            if (ModelState.IsValid && id == users.Id)
            {
                db.Entry(users).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // POST api/Users
        [HttpPost]
        public HttpResponseMessage PostUser(User users)
        {
            if (ModelState.IsValid)
            {
                db.User.Add(users);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, users);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = users.Id }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/Users/5
        [HttpDelete]
        public HttpResponseMessage DeleteUser(int id)
        {
            User users = db.User.Find(id);
            if (users == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.User.Remove(users);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, users);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}