using EVA.Models.ManageViewModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EVA.Models
{
    public class Site
    {
        public int SiteID { get; set; }

        public int Clientid { get; set; }

        [Required]
        [StringLength(80, MinimumLength = 3)]
        [Display(Name = "Site")]
        public string SiteName { get; set; }

        [Required]
        [Display(Name = "Facility Type")]
        public int SiteTypeID { get; set; }


        [Required]
        [StringLength(120, MinimumLength = 3)]
        [Display(Name = "Address")]
        public string SiteAddress { get; set; }
        [Required]
        [StringLength(80, MinimumLength = 3)]
        [Display(Name = "Suburb")]
        public string SiteSuburb { get; set; }
        [Required]
        [StringLength(8, MinimumLength = 3)]
        [Display(Name = "Post Code")]
        public string SitePostCode { get; set; }

        public SiteType SiteType { get; set; }

        public Client Client { get; set; }

        public ICollection<WaterBody> WaterBodys { get; set; }

        public ICollection<Job> Job { get; set; }

    }
}
