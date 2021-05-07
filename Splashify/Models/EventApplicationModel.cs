using System;
namespace Splashify.Models
{
    public class EventApplicationModel
    {
        public int ID { get; set; }
        public string eventID { get; set; }
        public int clubID { get; set; }
        public string clubname { get; set; }
        public string gender { get; set; }
        public string startdate { get; set; }

    }
}
