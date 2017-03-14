using System.Collections.Generic;

namespace EVA.Models
{
    public class Make
    {
        public int MakeID { get; set; }
        public string EqMake { get; set; }
        public ICollection<Equipment> Equipments { get; set; }

    }
}
