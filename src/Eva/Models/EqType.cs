using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EVA.Models
{
    public class EqType
    {
        public int EqTypeID { get; set; }
        public string EquipmentType { get; set; }

        public string EQFunction { get; set; }
        public ICollection<Equipment> Equipments { get; set; }

    }
}
