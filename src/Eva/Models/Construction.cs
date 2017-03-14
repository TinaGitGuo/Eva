using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EVA.Models
{
    public class Construction
    {
        public int ConstructionID { get; set; }
        public string iConstruction { get; set; }
        public ICollection<WaterBody> WaterBodys { get; set; }

    }
}
