using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EVA.Models
{
    public class HealthCategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Category")]
        public int CategoryID { get; set; }

        [Display(Name = "Category Description")]
        public string CategoryDescription { get; set; }

        [Display(Name = "Max Peak Bathing Load")]
        public decimal PeakLoad { get; set; }

        [Display(Name = "Max Turnover Time")]
        public decimal TurnoverTime { get; set; }

        [Display(Name = "Water allowance per turnover per person")]
        public decimal WaterAllowance { get; set; }

        [Display(Name ="Maximum Daily Bathing Load")]
        public decimal DailyLoad{get;set;}

        [Display(Name ="Sand Filter Max Flow Rate lpm")]
        public int Sand { get; set; }

        [Display(Name ="DE Filter Max Flow Rate lpm")]
        public int DE { get; set; }

        [Display(Name ="Cartridge Filter Max Flow Rate lpm")]
        public int Cartridge { get; set; }

        public ICollection<PoolType> PoolType { get; set; }
    


    }
}
