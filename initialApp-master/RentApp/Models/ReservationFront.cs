using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentApp.Models
{
    public class ReservationFront
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string StartBranch { get; set; }
        public string EndBranch { get; set; }
        public string Username { get; set; }
        public string CarName { get; set; }
        public string ServiceName { get; set; }

    }
}