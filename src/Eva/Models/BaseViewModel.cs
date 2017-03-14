using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Eva.Models
{
    public class BaseViewModel
    {
        [Display(Name ="Todays Date")]
        [DataType(DataType.Date)]
        public DateTime TodaysDate { get; set; }
    }
}
