using RentApp.Models.Entities;
using RentApp.Persistance.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace RentApp.Controllers
{
    public class AppUserController : ApiController
    {
        private readonly IUnitOfWork unitOfWork;

        public AppUserController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<AppUser> GetUsers()
        {
            return unitOfWork.Users.GetAll();
        }

        [ResponseType(typeof(AppUser))]
        public IHttpActionResult GetUser(int id)
        {
            AppUser user = unitOfWork.Users.Get(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult PutUser(int id, AppUser user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.Id)
            {
                return BadRequest();
            }

            try
            {
                unitOfWork.Users.Update(user);
                unitOfWork.Complete();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        [ResponseType(typeof(AppUser))]
        public IHttpActionResult PostUser(AppUser user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            unitOfWork.Users.Add(user);
            unitOfWork.Complete();

            return CreatedAtRoute("DefaultApi", new { id = user.Id }, user);
        }

        [ResponseType(typeof(AppUser))]
        public IHttpActionResult DeleteUser(int id)
        {
            AppUser user = unitOfWork.Users.Get(id);
            if (user == null)
            {
                return NotFound();
            }

            unitOfWork.Users.Remove(user);
            unitOfWork.Complete();

            return Ok(user);
        }

        private bool UserExists(int id)
        {
            return unitOfWork.Users.Get(id) != null;
        }
    }
}
