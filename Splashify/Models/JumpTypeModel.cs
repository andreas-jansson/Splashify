using System;
namespace Splashify.Models
{
    public class JumpTypeModel
    {
        public int id { get; set; }
        public int jumpID { get; set; }
        public int groupnr { get; set; }
        public string description { get; set; }
        public char style { get; set; }
        public float height { get; set; }
        public float value { get; set; }

    }
}
