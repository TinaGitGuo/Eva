using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EVA.Models
{
    public class HealthGroupToAid
    {
        public int ID { get; set; }

        public int GroupID { get; set; }

        public int FirstAidID { get; set; }

        public HealthGroup HealthGroup { get;set;}

        public HealthFirstAid HealthFirstAid { get; set; }

       //public ICollection<HealthFirstAid> HealthFirstAids { get; set; }
    }
}
 