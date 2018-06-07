using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RentApp.Models.Entities
{
    public class Service
    {
        
        public int Id { get; set; }
        public string Name { get; set; }
        public List<BranchOffice> BranchOffices { get; set; }
        public List<Vehicle> Vehicles { get; set; }
        public List<Review> Reviews { get; set; }
        public List<PriceList> PriceLists { get; set; }
        public AppUser User { get; set; }
        public int UserId { get; set; }

        public bool Approved { get; set; }
        public string Logo { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
    }
}