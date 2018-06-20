using RentApp.Models;
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
        public ApplicationUserManager UserManager { get; set; }

        public AppUserController(IUnitOfWork unitOfWork, ApplicationUserManager userManager)
        {
            UserManager = userManager;

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


        [HttpGet]
        [Route("api/AppUser/GetLogUser/{username}")]

        public IHttpActionResult GetLogUser(string username)
        {

            var user = UserManager.Users.FirstOrDefault(u => u.Id == username);
            AppUser userRet = new AppUser();
            List<AppUser> listaKorisnika = (List<AppUser>)unitOfWork.Users.GetAll();
            foreach(var u in listaKorisnika)
            {
                if (u.Email == user.Email)
                {
                    userRet = u;
                    break;
                }
            }
            if (userRet.Approved == true)
            {
                return Ok("true");
            }
            else
            {
                return Ok("false");

            }

        }



        [ResponseType(typeof(void))]
        public IHttpActionResult PutUser(UserFront user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            AppUser u1 = new AppUser();
            List<AppUser> listaUsera = (List<AppUser>)unitOfWork.Users.GetAll();
            foreach (var u in listaUsera)
            {
                if (u.Email == user.Email)
                {
                    u1 = u;
                    break;
                }
            }


            unitOfWork.Users.ApproveUser(u1.Id);
            unitOfWork.Complete();


            return Ok();
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
