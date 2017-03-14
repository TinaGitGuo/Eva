using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EVA.Models
{
    public class ClientContactDetail
    {
        public int ClientContactDetailID { get; set; }

        [Required]
        [Display(Name ="Contact Name")]

        public int ClientContactID { get; set; }

        [Required]
        [Display(Name = "Contact Type")]
        public int ContactTypeID { get; set; }

        [Required]
        [Display(Name = "Contact Detail")]

        public string ContactDetail { get; set; }

       public ICollection<ClientContact> ClientContact { get; set; }

       // public ClientContact ClientContacts { get; set; }

        public ContactType ContactType { get; set; }
    }
}
