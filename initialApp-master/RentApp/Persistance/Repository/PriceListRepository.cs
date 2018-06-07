using RentApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RentApp.Persistance.Repository
{
    public class PriceListRepository : Repository<PriceList, int>, IPriceListRepository
    {
        public PriceListRepository(DbContext context) : base(context)
        {
        }

        protected RADBContext DemoContext { get { return context as RADBContext; } }
    }
}