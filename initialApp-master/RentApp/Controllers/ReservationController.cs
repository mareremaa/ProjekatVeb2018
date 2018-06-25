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
    public class ReservationController : ApiController
    {
        private readonly IUnitOfWork unitOfWork;
        public ApplicationUserManager UserManager { get; set; }

        public ReservationController(IUnitOfWork unitOfWork, ApplicationUserManager userManager)
        {
            this.unitOfWork = unitOfWork;
            UserManager = userManager;

        }

        public IEnumerable<Reservation> GetReservations()
        {
            return unitOfWork.Reservations.GetAll();
        }

        [ResponseType(typeof(Reservation))]
        public IHttpActionResult GetReservation(int id)
        {
            Reservation reservation = unitOfWork.Reservations.Get(id);
            if (reservation == null)
            {
                return NotFound();
            }

            return Ok(reservation);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult PutReservation(int id, Reservation reservation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != reservation.ReservationId)
            {
                return BadRequest();
            }

            try
            {
                unitOfWork.Reservations.Update(reservation);
                unitOfWork.Complete();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservationExists(id))
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

        [ResponseType(typeof(Reservation))]
        public IHttpActionResult PostReservation(ReservationFront reservation)
        {
            lock (unitOfWork.Reservations)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                Reservation zaBazu = new Reservation();
                zaBazu.StartDate = reservation.StartDate;
                zaBazu.EndDate = reservation.EndDate;
                var user = UserManager.Users.FirstOrDefault(u => u.Id == reservation.Username);
                zaBazu.AppUserId = user.AppUserId;
                Service s1 = new Service();
                List<Service> listaServisa = (List<Service>)unitOfWork.Services.GetAll();
                foreach (var sc in listaServisa)
                {
                    if (sc.Name == reservation.ServiceName)
                    {
                        s1 = sc;
                        break;
                    }
                }
                Vehicle pom = new Vehicle();
                List<Vehicle> listaKola = (List<Vehicle>)unitOfWork.Vehicles.GetAll();
                foreach (var sc in listaKola)
                {
                    if (sc.Model == reservation.CarName && sc.ServiceId == s1.Id)
                    {
                        pom = sc;
                        break;
                    }
                }
                zaBazu.VehicleId = pom.VehicleId;

                List<BranchOffice> filijale = (List<BranchOffice>)unitOfWork.BranchOffices.GetAll();
                foreach (var fil in filijale)
                {
                    if (fil.Address == reservation.StartBranch)
                    {
                        zaBazu.BranchOfficeStartId = fil.BranchOfficeId;
                        break;
                    }
                }

                foreach (var fil in filijale)
                {
                    if (fil.Address == reservation.EndBranch)
                    {
                        zaBazu.BranchOfficeFinishId = fil.BranchOfficeId;
                        break;
                    }
                }
                List<Reservation> listaRezevacija = (List<Reservation>)unitOfWork.Reservations.GetAll();
                List<Reservation> listaRezevacija2 = new List<Reservation>();
                if (zaBazu.StartDate > zaBazu.EndDate)
                {
                    return Conflict();
                }
                foreach (var rez in listaRezevacija)
                {
                    if (rez.VehicleId == zaBazu.VehicleId)
                    {
                        if (zaBazu.StartDate > rez.StartDate && zaBazu.StartDate < rez.EndDate)
                        {
                            return Conflict();
                        }
                        if (zaBazu.EndDate > rez.StartDate && zaBazu.EndDate < rez.EndDate)
                        {
                            return Conflict();
                        }
                        if (zaBazu.StartDate < rez.StartDate && zaBazu.EndDate > rez.EndDate)
                        {
                            return Conflict();
                        }

                    }

                }

                unitOfWork.Reservations.Add(zaBazu);
                unitOfWork.Complete();
                return Ok();
            }
        }

        [ResponseType(typeof(Reservation))]
        public IHttpActionResult DeleteReservation(int id)
        {
            Reservation reservation = unitOfWork.Reservations.Get(id);
            if (reservation == null)
            {
                return NotFound();
            }

            unitOfWork.Reservations.Remove(reservation);
            unitOfWork.Complete();

            return Ok(reservation);
        }

        private bool ReservationExists(int id)
        {
            return unitOfWork.Reservations.Get(id) != null;
        }
    }
}
