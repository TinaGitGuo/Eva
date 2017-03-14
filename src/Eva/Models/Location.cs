using System.Collections.Generic;

namespace EVA.Models
{
    public class Location
    {
        public int LocationID { get; set; }
        public string iLocation { get; set; }
        public ICollection<WaterBody> WaterBodys { get; set; }

    }
}
