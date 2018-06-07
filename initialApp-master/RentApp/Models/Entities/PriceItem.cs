using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RentApp.Models.Entities
{
    public class PriceItem
    {
        public int PriceListId { get; set; }
        public PriceList PriceList { get; set; }
        public Vehicle Vehicle { get; set; }
        public int VehicleId { get; set; }

        [Key]
        public int PriceItemId { get; set; }
        public double HourPrice { get; set; }
        public bool Avaliable { get; set; }
    }
}