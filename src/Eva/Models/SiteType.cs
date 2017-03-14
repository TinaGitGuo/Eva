using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EVA.Models.ManageViewModels
{
    public class SiteType
    {
        public int SiteTypeID { get; set; }

        public string SType { get; set; }

        public int GroupID { get; set; }

        public HealthGroup HealthGroup { get; set; }
        public ICollection<HealthGroupToAid> HealthGroupToAids { get; set; }

        public ICollection<HealthGroupToKit> HealthGroupToKits { get; set; }
    }
}
