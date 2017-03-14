using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EVA.Models
{
    public class HealthFirstAid
    {
        [Key]
        public int FirstAidID { get; set; }

        [Required]
        [Display(Name ="Required Item")]
        public string FirstAidItem { get; set; }

        public ICollection<HealthGroupToAid> HealthGroupToAid { get; set; }
    }
}
