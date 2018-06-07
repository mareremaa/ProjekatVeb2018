using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RentApp.Models.Entities
{
    public class Reservation
    {
        public AppUser User { get; set; }
        public int UserId {get; set;}
        public BranchOffice BranchOfficeStart { get; set; }
        public BranchOffice BranchOfficeFinish { get; set; }
        public int BranchOfficeStartId { get; set; }
        public int BranchOfficeFinishId { get; set; }
        public Vehicle Vehicle { get; set; }
        public int VehicleId { get; set; }
        [Key]
        public int ReservationId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}