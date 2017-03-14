using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EVA.Models
{
    public class HealthToilet
    {
        public int HealthToiletID { get; set; }

        [Required]
        [Display(Name ="Loading Factor")]

        public decimal LoadingFactor { get; set; }

        
        public int GroupID { get; set; }

        [Display (Name ="Female Water Closets")]

        public int FemaleWaterCloset { get; set; }

        [Display(Name ="Male Water Closets")]
        public int MaleWaterCloset { get; set; }

        [Display(Name ="Male Urinals")]
        public int MaleUrinals { get; set; }

        [Display(Name ="Showers Required")]
        public int ShowersPerPatron { get; set; }

        public int Handbasins { get; set; }

        public ICollection<HealthGroup> HealthGroup { get; set; }
    }
}
