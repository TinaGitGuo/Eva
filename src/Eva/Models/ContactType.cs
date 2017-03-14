using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EVA.Models
{
    public class ContactType
    {
        public int ContactTypeID { get; set; }

        [Required]
        [Display(Name ="Contact Type")]

        public string iContactType { get; set; }

        public ClientContactDetail ClientContactDetail { get; set; }
    }
}
