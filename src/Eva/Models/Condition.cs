using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EVA.Models
{



    public class Condition
    {
        public int ConditionID { get; set; }

        [MaxLength(26)]
        public string EqCondition { get; set; }
        public ICollection<Equipment> Equipments { get; set; }

    }
}