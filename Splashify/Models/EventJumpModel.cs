using System;
namespace Splashify.Models
{
    public class EventJumpModel
    {
        public int jumpID { get; set; }
        public string eventID { get; set; }
        public int competitorID { get; set; }
        public string fname { get; set; }
        public string lname { get; set; }
        public int jumpnr { get; set; }
        public int judgeID { get; set; }
        public int score { get; set; }
        public int finalscore { get; set; }

    }
}
