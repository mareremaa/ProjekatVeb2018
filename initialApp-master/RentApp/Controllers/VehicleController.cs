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
    public class VehicleController : ApiController
    {
        private readonly IUnitOfWork unitOfWork;

        public VehicleController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<Vehicle> GetVehicles()
        {
            return unitOfWork.Vehicles.GetAll();
        }



        [HttpGet]
        [Route("api/Vehicle/GetServiceVehicles/{serviceName}")]
        public IEnumerable<Vehicle> GetServiceVehicles(string serviceName)
        {
            return unitOfWork.Vehicles.GetServiceVehicles(serviceName);
        }


        [ResponseType(typeof(Vehicle))]
        public IHttpActionResult GetVehicle(int id)
        {
            Vehicle vehicle = unitOfWork.Vehicles.Get(id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return Ok(vehicle);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult PutVehicle(int id, Vehicle vehicle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vehicle.VehicleId)
            {
                return BadRequest();
            }

            try
            {
                unitOfWork.Vehicles.Update(vehicle);
                unitOfWork.Complete();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VehicleExists(id))
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

        [ResponseType(typeof(Vehicle))]
        public IHttpActionResult PostVehicle(CarFront vehicle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            List<Service> listaServisa = (List<Service>)unitOfWork.Services.GetAll();

            Vehicle zaBazu = new Vehicle();
            zaBazu.Description = vehicle.Description;
            zaBazu.Maker = vehicle.Maker;
            zaBazu.Model = vehicle.Model;
            zaBazu.YearOfMaking = vehicle.YearOfMaking;
            foreach (var sc in listaServisa)
            {
                if (sc.Name == vehicle.ServiceName)
                {
                    zaBazu.ServiceId = sc.Id;
                    break;
                }
            }


            unitOfWork.Vehicles.Add(zaBazu);
            unitOfWork.Complete();

            return CreatedAtRoute("DefaultApi", new { id = zaBazu.VehicleId }, zaBazu);
        }

        [ResponseType(typeof(Vehicle))]
        public IHttpActionResult DeleteVehicle(int id)
        {
            Vehicle vehicle = unitOfWork.Vehicles.Get(id);
            if (vehicle == null)
            {
                return NotFound();
            }

            unitOfWork.Vehicles.Remove(vehicle);
            unitOfWork.Complete();

            return Ok(vehicle);
        }

        private bool VehicleExists(int id)
        {
            return unitOfWork.Vehicles.Get(id) != null;
        }
    }
}
