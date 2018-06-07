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
    public class ReviewController : ApiController
    {
        private readonly IUnitOfWork unitOfWork;

        public ReviewController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<Review> GetReviews()
        {
            return unitOfWork.Reviews.GetAll();
        }

        [ResponseType(typeof(Review))]
        public IHttpActionResult GetReview(int id)
        {
            Review review = unitOfWork.Reviews.Get(id);
            if (review == null)
            {
                return NotFound();
            }

            return Ok(review);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult PutReview(int id, Review review)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != review.ServiceId)
            {
                return BadRequest();
            }

            try
            {
                unitOfWork.Reviews.Update(review);
                unitOfWork.Complete();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReviewExists(id))
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

        [ResponseType(typeof(Review))]
        public IHttpActionResult PostReview(Review review)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            unitOfWork.Reviews.Add(review);
            unitOfWork.Complete();

            return CreatedAtRoute("DefaultApi", new { id = review.ReviewId }, review);
        }

        [ResponseType(typeof(Review))]
        public IHttpActionResult DeleteReview(int id)
        {
            Review review = unitOfWork.Reviews.Get(id);
            if (review == null)
            {
                return NotFound();
            }

            unitOfWork.Reviews.Remove(review);
            unitOfWork.Complete();

            return Ok(review);
        }

        private bool ReviewExists(int id)
        {
            return unitOfWork.Reviews.Get(id) != null;
        }
    }
}
