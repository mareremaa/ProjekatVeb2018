using RentApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RentApp.Persistance.Repository
{
    public class BranchOfficeRepository : Repository<BranchOffice, int>, IBranchOfficeRepository
    {
        public BranchOfficeRepository(DbContext context) : base(context)
        {
        }

        protected RADBContext DemoContext { get { return context as RADBContext; } }
    }
}