using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EVA.Models
{
    public class Position
    {
        public int PositionID { get; set; }

        [Required]
        [Display(Name ="Position")]
        public string iPosition { get; set; }

        public ICollection<ClientContact> ClientContact { get; set; }
    }
}
