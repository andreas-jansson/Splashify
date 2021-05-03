using System;

//Collection all models in one in order to use multiple in the same view
namespace Splashify.Models
{
    public class IndexModel
    {
        public UserModel UserModel { get; set; }
        public ScoreModel scoreModel { get; set; }
        public EventModel eventModel { get; set; }
        public LoginModel logintModel { get; set; }
        public SearchModel SearchModel { get; set; }
    }
}

