using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EVA.Models
{
    public class WaterBody
    {
        public int WaterBodyID { get; set; }

        [Display(Name = "Site")]
        public int SiteID { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Water Body Name")]
        public string WBName { get; set; }


        [Display(Name = "Location")]
        public int LocationID { get; set; }

        [Display(Name = "Pool Type")]
        public int PoolTypeID { get; set; }


        [Display(Name = "Construction")]
        public int ConstructionID { get; set; }

        public decimal Length { get; set; }
        public decimal Width { get; set; }
        public decimal Depth { get; set; }


       
        public Site Site { get; set; }
       
        public Construction Construction { get; set; }
        public Location Location { get; set; }

        public PoolType PoolType { get; set; }
        public ICollection<Equipment> Equipment { get; set; }

        //public ICollection<Jobs> Jobs { get; set; }

       

        [Display(Name = "Volume")]
        public decimal PoolVolume
        {
            get
            {
                return Length * Width * Depth;
            }
        }

        [Display(Name = "Area")]

        public decimal PoolArea
        {
            get
            {
                return Length * Width;
            }
        }

    }
}
