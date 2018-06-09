using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RentApp.Models.Entities
{
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }

        [ForeignKey("Service")]
        public int ServiceId { get; set; }
        public Service Service { get; set; }

        [ForeignKey("AppUser")]

        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
       

        public int Score { get; set; }
        public string DescriptionScore { get; set; }
    }
}