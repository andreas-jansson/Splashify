using System;
using System.ComponentModel.DataAnnotations;

namespace Splashify.Models
{
    public class ClubModel
    {
        public int clubID { get; set; }
        [Required]
        public int userID { get; set; }
        [Required]
        public string clubname { get; set; }
    }
}
