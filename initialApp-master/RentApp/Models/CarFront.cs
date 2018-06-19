using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentApp.Models
{
    public class CarFront
    {
        public string Model { get; set; }
        public string Maker { get; set; }
        public DateTime YearOfMaking { get; set; }
        public string Description { get; set; }
        public string ServiceName { get; set; }
    }
}