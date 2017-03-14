using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EVA.Models
{
    public class Division
    {
        public int DivisionID { get; set; }

        public string iDivision { get; set; }

        public ICollection<Department> Deparments { get; set; }

        public ICollection<Staff> Staff { get; set; }

        public ICollection<Job> Job { get; set; }
    }
}
