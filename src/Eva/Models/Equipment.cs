using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EVA.Models
{
    public class Equipment
    {
        [Key]
        public int EQDataID { get; set; }


        [Display(Name = "Inspection Date")]
        [DataType(DataType.Date)]
        public DateTime InspectDate { get; set; } 

        //[Display(Name = "Site")]
        //public int SiteID { get; set; }

        [Display(Name = "Water Body")]
        public int WaterBodyID { get; set; }

        [Display(Name = "Equipment Type")]
        public int EqTypeID { get; set; }

        [Display(Name = "Manufacturer")]
        public int MakeID { get; set; }

        [Required]
        public string Model { get; set; }


        [Display(Name = "Serial Number")]
        public string SerialNumber { get; set; }


        [Display(Name = "Estimated Install Date")]
        [DataType(DataType.Date)]
        public DateTime? InstallDate { get; set; }

        //[Required(ErrorMessage = "Please Upload a Valid Image File")]
        //[DataType(DataType.Upload)]
        //[Display(Name = "Upload Image")]
       // [FileExtensions(Extensions = "jpg,png,gif,jpeg,bmp,svg")]
       // public IFormFile Content { get; set; }


        [Display(Name = "Estimated Remaining Life")]
        public int EstimateRemainingLife { get; set; }

        [Display(Name = "Last Service Date")]
        [DataType(DataType.Date)]
        public DateTime? LastServiceDate { get; set; }

        [Display(Name = "What is Your Recommendation")]
        public string Recommended { get; set; }
        public string Notes { get; set; }

        [Display(Name = "Condition")]
        public int ConditionID { get; set; }

        [Display(Name = "Replacement Cost")]
        [DataType(DataType.Currency)]
        public float? ReplacementCost { get; set; }


       // public Site Site { get; set; }
        public WaterBody Waterbody { get; set; }
        public EqType Eqtype { get; set; }
        public Make Make { get; set; }

        public Condition Condition { get; set; }

        public DateTime returnDate
        {
            get
            {
                return DateTime.Today;
            }
        }


    }
}
