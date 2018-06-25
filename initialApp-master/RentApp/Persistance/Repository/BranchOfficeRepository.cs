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

        public IEnumerable<BranchOffice> GetSomeBranches(string serviceName)
        {
            List<BranchOffice> BranchRet = new List<BranchOffice>();
            List<BranchOffice> BranchAll = RAContext.BranchOffices.ToList();
            Service service = RAContext.Services.FirstOrDefault(s => s.Name == serviceName);
            foreach(var br in BranchAll)
            {
                if (br.ServiceId == service.Id)
                {
                    BranchRet.Add(br);
                }
            }
            return BranchRet;
        }

        protected RADBContext RAContext
        {
            get => context as RADBContext;
        }
    }
}