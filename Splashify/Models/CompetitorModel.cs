using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Splashify.Models
{
    public class CompetitorModel
    {

        public int competitorID { get; set; }
        public int userID { get; set; }
        public string eventID { get; set; }
        public int jumpnr { get; set; }
        public List<SelectListItem> eventList{ get; set; }
        public List<string> eventListstring { get; set; }

    }
}
