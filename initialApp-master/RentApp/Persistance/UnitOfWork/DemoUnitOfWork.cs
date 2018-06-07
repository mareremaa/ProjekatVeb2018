using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using RentApp.Persistance.Repository;
using Unity.Attributes;

namespace RentApp.Persistance.UnitOfWork
{
    public class DemoUnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;
        [Dependency]
        public IVehicleRepository Vehicles { get; set; }
        [Dependency]
        public IReviewRepository Reviews { get; set; }
        [Dependency]
        public IServiceRepository Services { get; set; }
        [Dependency]
        public IReservationRepository Reservations { get; set; }
        [Dependency]
        public IPriceListRepository PriceLists { get; set; }
        [Dependency]
        public IPriceItemRepository PriceItems { get; set; }
        [Dependency]
        public IBranchOfficeRepository BranchOffices { get; set; }
        [Dependency]
        public IUserRepository Users { get; set; }





        public DemoUnitOfWork(DbContext context)
        {
            _context = context;
        }
        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}