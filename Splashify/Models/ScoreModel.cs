using System;
namespace Splashify.Models
{
    public class ScoreModel
    {
        public string eventID { get; set; }
        public int judgeID { get; set; }
        public int competitorID { get; set; }
        public int userID { get; set; }
        public int jumpnr { get; set; }
        public int jumpID { get; set; }
        public int jumptype { get; set; }
        public float height { get; set; }
        public float eventtype { get; set; }
        public float score { get; set; }
        public DateTime startdate { get; set; }
    }
}
