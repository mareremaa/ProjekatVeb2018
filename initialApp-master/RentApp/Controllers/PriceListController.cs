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
    public class PriceListController : ApiController
    {
        private readonly IUnitOfWork unitOfWork;

        public PriceListController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<PriceList> GetPriceLists()
        {
            return unitOfWork.PriceLists.GetAll();
        }

        [ResponseType(typeof(PriceList))]
        public IHttpActionResult GetPriceList(int id)
        {
            PriceList pricelist = unitOfWork.PriceLists.Get(id);
            if (pricelist == null)
            {
                return NotFound();
            }

            return Ok(pricelist);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult PutPriceList(int id, PriceList pricelist)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pricelist.PriceListId)
            {
                return BadRequest();
            }

            try
            {
                unitOfWork.PriceLists.Update(pricelist);
                unitOfWork.Complete();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PriceListExists(id))
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

        [ResponseType(typeof(PriceList))]
        public IHttpActionResult PostPriceList(PriceList pricelist)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            unitOfWork.PriceLists.Add(pricelist);
            unitOfWork.Complete();

            return CreatedAtRoute("DefaultApi", new { id = pricelist.PriceListId }, pricelist);
        }

        [ResponseType(typeof(PriceList))]
        public IHttpActionResult DeletePriceList(int id)
        {
            PriceList pricelist = unitOfWork.PriceLists.Get(id);
            if (pricelist == null)
            {
                return NotFound();
            }

            unitOfWork.PriceLists.Remove(pricelist);
            unitOfWork.Complete();

            return Ok(pricelist);
        }

        private bool PriceListExists(int id)
        {
            return unitOfWork.PriceLists.Get(id) != null;
        }
    }
}
