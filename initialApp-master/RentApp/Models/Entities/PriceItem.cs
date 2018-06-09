using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RentApp.Models.Entities
{
    public class PriceItem
    {
        [Key]
        public int PriceItemId { get; set; }
        [ForeignKey("PriceList")]
        public int PriceListId { get; set; }
        public PriceList PriceList { get; set; }

        [ForeignKey("Vehicle")]
        public int VehicleId { get; set; }

        public Vehicle Vehicle { get; set; }

        
        public double HourPrice { get; set; }
        public bool Avaliable { get; set; }
    }
}