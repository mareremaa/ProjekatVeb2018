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
    public class PriceItemController : ApiController
    {
        private readonly IUnitOfWork unitOfWork;

        public PriceItemController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<PriceItem> GetPriceItems()
        {
            return unitOfWork.PriceItems.GetAll();
        }

        [ResponseType(typeof(PriceItem))]
        public IHttpActionResult GetPriceItem(int id)
        {
            PriceItem priceitem = unitOfWork.PriceItems.Get(id);
            if (priceitem == null)
            {
                return NotFound();
            }

            return Ok(priceitem);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult PutPriceItem(int id, PriceItem priceitem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != priceitem.PriceItemId)
            {
                return BadRequest();
            }

            try
            {
                unitOfWork.PriceItems.Update(priceitem);
                unitOfWork.Complete();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PriceItemExists(id))
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

        [ResponseType(typeof(PriceItem))]
        public IHttpActionResult PostPriceItem(PriceItem priceitem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            unitOfWork.PriceItems.Add(priceitem);
            unitOfWork.Complete();

            return CreatedAtRoute("DefaultApi", new { id = priceitem.PriceItemId }, priceitem);
        }

        [ResponseType(typeof(PriceItem))]
        public IHttpActionResult DeletePriceItem(int id)
        {
            PriceItem priceitem = unitOfWork.PriceItems.Get(id);
            if (priceitem == null)
            {
                return NotFound();
            }

            unitOfWork.PriceItems.Remove(priceitem);
            unitOfWork.Complete();

            return Ok(priceitem);
        }

        private bool PriceItemExists(int id)
        {
            return unitOfWork.PriceItems.Get(id) != null;
        }
    }
}
