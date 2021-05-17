using System;
using System.ComponentModel.DataAnnotations;

namespace Splashify.Models
{
    public class CreateEventModel
    {
        [Required]
        public string eventID { get; set; }
        [Required]
        public DateTime startdate { get; set; }
        [Required]
        public string gender { get; set; }
        [Required]
        public int judge1ID { get; set; }
        [Required]
        public int judge2ID { get; set; }
        [Required]
        public int judge3ID { get; set; }
        [Required]
        public string eventtype { get; set; }

    }
}
