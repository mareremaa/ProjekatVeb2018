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

        public BranchOfficeController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<BranchOffice> GetBranchOffices()
        {
            return unitOfWork.BranchOffices.GetAll();
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
        public IHttpActionResult PostBranchOffice(BranchOffice off)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            unitOfWork.BranchOffices.Add(off);
            unitOfWork.Complete();

            return CreatedAtRoute("DefaultApi", new { id = off.BranchOfficeId }, off);
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
