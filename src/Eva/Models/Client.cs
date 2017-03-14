using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EVA.Models
{
    public class Client
    {
        public int ClientID { get; set; }

        public string ShentonAcc { get; set; }

        [Required]
        [StringLength(120, MinimumLength = 3)]
        [Display(Name="Business Name")]
        public string Name { get; set; }

        [Required]
        [StringLength(120, MinimumLength = 3)]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Required]
        [StringLength(120, MinimumLength = 3)]
        public string Suburb { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 3)]
        [Display (Name="Post Code")]
        public string PostCode { get; set; }


        [Required]
        [Display(Name = "Account Email")]
        public string AccountEmail { get; set; }

        [Required]
        [Display(Name = "Account Contact")]
        public string AccountContact { get; set; }


        [Required]
        [StringLength(20, MinimumLength = 3)]
        [Display(Name = "Office Phone")]
        public string OfficePhone { get; set; }

        public ICollection<Site> Site { get; set; }

        public ICollection<ClientContact> ClientContact { get; set; }

    }
}
