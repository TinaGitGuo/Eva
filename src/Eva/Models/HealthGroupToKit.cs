using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EVA.Models
{
    public class HealthGroupToKit
    {
        public int ID { get; set; }

        public int GroupID { get; set; }

        public int HealthID { get; set; }


        public HealthGroup HealthGroup { get; set; }

        public HealthFirstAidKit HealthFirstAidKit { get; set; }
    }
}
