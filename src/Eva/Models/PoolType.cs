using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EVA.Models
{
    public class PoolType
    {
        public int PoolTypeID { get; set; }
        public string iPoolType { get; set; }

        public int CategoryID { get; set; }

        public HealthCategory HealthCategory { get; set; }
        public ICollection<WaterBody> WaterBody { get; set; }

    }
}
