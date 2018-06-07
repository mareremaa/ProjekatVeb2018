using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RentApp.Models.Entities
{
    public class Review
    {
        public int ServiceId { get; set; }
        public Service Service { get; set; }
        public int UserId { get; set; }
        public AppUser User { get; set; }
        [Key]
        public int ReviewId { get; set; }

        public int Score { get; set; }
        public string DescriptionScore { get; set; }
    }
}