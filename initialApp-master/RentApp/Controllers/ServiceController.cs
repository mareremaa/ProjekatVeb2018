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
using RentApp.Models;
using RentApp.Models.Entities;
using RentApp.Persistance;
using RentApp.Persistance.UnitOfWork;

namespace RentApp.Controllers
{
    [RoutePrefix("api/Services")]
    public class ServiceController : ApiController
    {
        private readonly IUnitOfWork unitOfWork;
        public ApplicationUserManager UserManager { get; set; }
        public ServiceController(IUnitOfWork unitOfWork, ApplicationUserManager userManager)
        {
            UserManager = userManager;
            this.unitOfWork = unitOfWork;
        }
        public IEnumerable<Service> GetServices()
        {
            return unitOfWork.Services.GetAll();
        }

        [ResponseType(typeof(Service))]
        public IHttpActionResult GetService(int id)
        {
            Service service = unitOfWork.Services.Get(id);
            if (service == null)
            {
                return NotFound();
            }

            return Ok(service);
        }
        
        [ResponseType(typeof(void))]
        public IHttpActionResult PutService(Service service)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            
            try
            {
               
                unitOfWork.Services.Update(service);
                unitOfWork.Complete();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServiceExists(service.Id))
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

        [ResponseType(typeof(Service))]
        public IHttpActionResult PostService(ServiceFront service)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = UserManager.Users.FirstOrDefault(u => u.Id == service.UserName);
            Service zaUpis = new Service();
            zaUpis.AppUserId = user.AppUserId;
            zaUpis.Description = service.Description;
            zaUpis.Email = service.Email;
            zaUpis.Name = service.Name;
            unitOfWork.Services.Add(zaUpis);
            unitOfWork.Complete();

            return CreatedAtRoute("DefaultApi", new { id = zaUpis.Id }, zaUpis);
        }

        [ResponseType(typeof(Service))]
        public IHttpActionResult DeleteService(int id)
        {
            Service service = unitOfWork.Services.Get(id);
            if (service == null)
            {
                return NotFound();
            }

            unitOfWork.Services.Remove(service);
            unitOfWork.Complete();

            return Ok(service);
        }

        private bool ServiceExists(int id)
        {
            return unitOfWork.Services.Get(id) != null;
        }
    }
}