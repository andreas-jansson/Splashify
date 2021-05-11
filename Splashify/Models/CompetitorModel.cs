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
        [BindProperty]
        public string eventID { get; set; }
        public int jumpnr { get; set; }
        [HtmlAttributeName("asp-items")]
        public List<SelectListItem> eventList{ get; set; }

    }
}
