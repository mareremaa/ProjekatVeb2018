using RentApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RentApp.Persistance.Repository
{
    public class PriceItemRepository : Repository<PriceItem, int>, IPriceItemRepository
    {
        public PriceItemRepository(DbContext context) : base(context)
        {
        }

        protected RADBContext DemoContext { get { return context as RADBContext; } }
    }
}