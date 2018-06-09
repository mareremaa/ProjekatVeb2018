using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RentApp.Models.Entities
{
    public class Reservation
    {
        [Key]
        public int ReservationId { get; set; }

        [ForeignKey("AppUser")]
        public int AppUserId { get; set; }

        public AppUser AppUser { get; set; }

        [ForeignKey("BranchOfficeStart")]
        public int BranchOfficeStartId { get; set; }

        public BranchOffice BranchOfficeStart { get; set; }
        [ForeignKey("BranchOfficeFinish")]
        public int BranchOfficeFinishId { get; set; }


        public BranchOffice BranchOfficeFinish { get; set; }


        [ForeignKey("Vehicle")]

        public int VehicleId { get; set; }

        public Vehicle Vehicle { get; set; }
       
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}