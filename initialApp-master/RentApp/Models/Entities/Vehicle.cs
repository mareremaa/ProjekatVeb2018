using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RentApp.Models.Entities
{
    public class Vehicle
    {
        public int ServiceId { get; set; }
        public Service Service { get; set; }
        public List<PriceItem> PriceItems { get; set; }
        public List<Reservation> Reservations { get; set; }

        [Key]
        public int VehicleId { get; set; }
        public string Model { get; set; }
        public string Maker { get; set; }
        public DateTime YearOfMaking { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        
    }
}