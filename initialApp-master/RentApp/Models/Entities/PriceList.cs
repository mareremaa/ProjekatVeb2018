using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RentApp.Models.Entities
{
    public class PriceList
    {
        [Key]
        public int PriceListId { get; set; }

        [ForeignKey("Service")]
        public int ServiceId { get; set; }
        public Service Service { get; set; }
        public List<PriceItem> Items { get; set; }
       
    }
}