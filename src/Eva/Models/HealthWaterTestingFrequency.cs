using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EVA.Models
{
    public class HealthWaterTestingFrequency
    {
        public int ID { get; set; }

        public int GroupID { get; set; }

        [Required]
        public string Frequency { get; set; }

        public HealthGroup HealthGroup { get; set; }
    }
}
