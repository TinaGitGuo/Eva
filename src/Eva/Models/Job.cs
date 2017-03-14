using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EVA.Models
{
    public class Job
    {
        public int JobID { get; set; }

        [Required]
        [Display(Name = "Job Number")]
        public int JobNumber { get; set; }

        [Required]
        [Display(Name = "Site")]
        public int SiteID { get; set; }

        [Display(Name = "Water Body")]
        public int WaterBodyID { get; set; }

        [Required]
        [Display(Name = "Client Order")]
        public string OrderNumber { get; set; }

        [Required]
        [Display(Name = "Booking Date")]
        public DateTime BookingDate { get; set; }

        [Required]
        public int DepartmentID { get; set; }

        [Required]
        [Display(Name = "Short Job Description")]
        [StringLength(900, MinimumLength = 20)]
        public string JobDescription { get; set; }


        public DateTime? InvoiceDate { get; set; }

        public Site Site { get; set; }
    }
}
