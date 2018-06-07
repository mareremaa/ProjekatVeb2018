using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RentApp.Models.Entities
{
    public class PriceList
    {
        public int ServiceId { get; set; }
        public Service Service { get; set; }
        public List<PriceItem> Items { get; set; }
        [Key]
        public int PriceListId { get; set; }
    }
}