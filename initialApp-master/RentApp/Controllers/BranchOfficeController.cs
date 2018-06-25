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
    public class BranchOfficeController : ApiController
    {
        private readonly IUnitOfWork unitOfWork;
        public ApplicationUserManager UserManager { get; set; }

        public BranchOfficeController(IUnitOfWork unitOfWork, ApplicationUserManager userManager)
        {
            UserManager = userManager;
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<BranchOffice> GetBranchOffices()
        {
            return unitOfWork.BranchOffices.GetAll();
        }

        [HttpGet]
        [Route("api/BranchOffice/GetSomeBranch/{serviceName}")]
        public IEnumerable<BranchOffice> GetSomeBranch(string serviceName)
        {
            return unitOfWork.BranchOffices.GetSomeBranches(serviceName);



        }




        [ResponseType(typeof(BranchOffice))]
        public IHttpActionResult GetBranchOffice(int id)
        {
            BranchOffice off = unitOfWork.BranchOffices.Get(id);
            if (off == null)
            {
                return NotFound();
            }

            return Ok(off);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult PutBranchOffice(int id, BranchOffice off)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != off.BranchOfficeId)
            {
                return BadRequest();
            }

            try
            {
                unitOfWork.BranchOffices.Update(off);
                unitOfWork.Complete();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BranchOfficeExists(id))
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

        [ResponseType(typeof(BranchOffice))]
        public IHttpActionResult PostBranchOffice(BranchOfficeFront off)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            List<Service> listaServisa = (List<Service>)unitOfWork.Services.GetAll();
            BranchOffice zaBazu = new BranchOffice();
            zaBazu.Address = off.Address;
            foreach(var sc in listaServisa)
            {
                if (sc.Name == off.ServiceName)
                {
                    zaBazu.ServiceId = sc.Id;
                    break;
                }
            }

            List<BranchOffice> sviBranc = (List<BranchOffice>)unitOfWork.BranchOffices.GetAll();
            foreach (var sc in sviBranc)
            {
                if (sc.Address == zaBazu.Address)
                {
                    return Conflict();
                }
            }

            unitOfWork.BranchOffices.Add(zaBazu);
            unitOfWork.Complete();

            return Ok();
        }

        [ResponseType(typeof(BranchOffice))]
        public IHttpActionResult DeleteBranchOffice(int id)
        {
            BranchOffice off = unitOfWork.BranchOffices.Get(id);
            if (off == null)
            {
                return NotFound();
            }

            unitOfWork.BranchOffices.Remove(off);
            unitOfWork.Complete();

            return Ok(off);
        }

        private bool BranchOfficeExists(int id)
        {
            return unitOfWork.BranchOffices.Get(id) != null;
        }
    }
}
