using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EVA.Models
{
    public class HealthGroup
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name="Group")]
        public int GroupID { get; set; }

        [Display(Name ="Patron Access Limitations")]
        public string AccessLimit { get; set; }

        public string Activity { get; set; }

        [Display(Name ="Technical Operator Requirement")]
        public string TechnicalOperator { get; set; }

        [Display(Name ="Patron Supervision")]
        public string PatronSupervision { get; set; }

        [Display(Name ="Emergency Care")]
        public string EmergencyCare { get; set; }

        public ICollection<HealthGroupToAid> HealthGroupToAid { get; set; }

        public ICollection<HealthGroupToKit> HEalthGroupToKit { get; set; }

        public HealthToilet HealthToilet { get; set; }

        public ICollection<HealthWaterTestingFrequency> HealthWaterTestingFrequency { get; set; }
    } 
}
