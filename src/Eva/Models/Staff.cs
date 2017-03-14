using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EVA.Models
{
    public class Staff
    {
        public int StaffID { get; set; }

        [Required]
        [Display(Name ="Division")]
        public int DivisionID { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string StaffName { get; set; }

        [Required]
        [Display(Name ="Hire Date")]
        public DateTime HireDate { get; set; }


        [Display(Name ="Leaving Date")]
        public DateTime TerminationDate { get; set; }

        public Division Division { get; set; }
    }
}
