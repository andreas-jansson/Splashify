using System;
using System.ComponentModel.DataAnnotations;

namespace Splashify.Models
{
    
    public class SearchModel
    {
        public string SearchField { get; set; }
        public int Value { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string startdate { get; set; }
        public string JudgeID { get; set; }
        public string EventID { get; set; }
        public string CompetitorID { get; set; }
        public string JumpID { get; set; }
        public string Jumpnr { get; set; }
        public string Finalscore { get; set; }
    }

}
