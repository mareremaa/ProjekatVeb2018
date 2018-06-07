using RentApp.Persistance.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentApp.Persistance.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IVehicleRepository Vehicles { get; set; }
        IReviewRepository Reviews { get; set; }

        IServiceRepository Services { get; set; }
        IReservationRepository Reservations { get; set; }
        IPriceListRepository PriceLists { get; set; }
        IPriceItemRepository PriceItems { get; set; }
        IBranchOfficeRepository BranchOffices { get; set; }
        IUserRepository Users { get; set; }
        int Complete();
    }
}
