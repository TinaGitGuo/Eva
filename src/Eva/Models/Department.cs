using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EVA.Models
{
    public class Department
    {
        public int DepartmentID { get; set; }

        [Required]
        [Display(Name ="Division")]

        public int DivisionID { get; set; }

        [Required]
        [Display(Name = "Department")]
        public string iDepartment { get; set; }

        public Division Division { get; set; }

        public ICollection<Job> Job { get; set; }
    }
}
