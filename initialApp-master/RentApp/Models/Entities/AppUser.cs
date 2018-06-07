using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RentApp.Models.Entities
{
    public class AppUser
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public List<Service> Services { get; set; }
        public List<Review> Reviews { get; set; }
        public List<Reservation> Reservations { get; set; }

        public string Email { get; set; }
        public DateTime DateBirth { get; set; }
        public string Image { get; set; }
        public bool Approved { get; set; }
        public bool CanCreate { get; set; }
        
    }
}