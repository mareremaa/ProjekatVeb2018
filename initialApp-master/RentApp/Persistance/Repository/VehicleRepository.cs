using RentApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RentApp.Persistance.Repository
{
    public class VehicleRepository : Repository<Vehicle, int>, IVehicleRepository
    {
        public VehicleRepository(DbContext context) : base(context)
        {
        }
       
        protected RADBContext DemoContext { get { return context as RADBContext; } }

        public IEnumerable<Vehicle> GetServiceVehicles(string serviceName)
        {
            List<Vehicle> serviceVehicles = new List<Vehicle>();
            List<Vehicle> serviceVehiclesRet = new List<Vehicle>();

            serviceVehicles = RAContext.Vehicles.ToList();
            Service service = RAContext.Services.FirstOrDefault(s => s.Name == serviceName);
            foreach(var v in serviceVehicles)
            {
                if (v.ServiceId == service.Id)
                {
                    serviceVehiclesRet.Add(v);
                }
            }
            return serviceVehiclesRet;
        }
        protected RADBContext RAContext
        {
            get => context as RADBContext;
        }
    }
}