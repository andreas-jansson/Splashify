using System;

//Collection all models in one in order to use multiple in the same view
namespace Splashify.Models
{
    public class IndexModel
    {
        public ScoreModel personModel { get; set; }
        public ScoreModel scoreModel { get; set; }  
    }
}

