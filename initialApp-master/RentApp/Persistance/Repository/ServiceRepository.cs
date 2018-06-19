using RentApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RentApp.Persistance.Repository
{
    public class ServiceRepository: Repository<Service, int>, IServiceRepository
    {
        public ServiceRepository(DbContext context) : base(context)
        {
        }

        protected RADBContext DemoContext { get { return context as RADBContext; } }

        public void ApproveService(int id)
        {
            var service = RAContext.Services.FirstOrDefault(s => s.Id == id);
            service.Approved = true;
            RAContext.Entry(service).State = EntityState.Modified;
        }

        protected RADBContext RAContext
        {
            get => context as RADBContext;
        }
    }
}