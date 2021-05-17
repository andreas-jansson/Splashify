using System;
namespace Splashify.Models
{
    public class EventJumpModel
    {
        public int jumpID { get; set; }
        public string eventID { get; set; }
        public int competitorID { get; set; }
        public int userID { get; set; }
        public string fname { get; set; }
        public string lname { get; set; }
        public int jumpnr { get; set; }
        public string jumptype { get; set; }
        public int judgeID { get; set; }
        public string JudgeFirstName { get; set; }
        public string JudgeLastName { get; set; }
        public float score { get; set; }
        public float finalscore { get; set; }

    }
}
