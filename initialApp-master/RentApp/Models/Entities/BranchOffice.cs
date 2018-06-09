using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RentApp.Models.Entities
{
    public class BranchOffice
    {
        [Key]
        public int BranchOfficeId { get; set; }

        [ForeignKey("Service")]
        public int ServiceId { get; set; }
        public Service Service { get; set; }
        public List<Reservation> ReservationsStart { get; set; }
        public List<Reservation> ReservationsFinish { get; set; }

        public string Image { get; set; }
        public string Address { get; set; }
    }
}