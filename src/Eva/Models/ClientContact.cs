using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EVA.Models
{
    public class ClientContact
    {
        public int ClientContactID { get; set; }

        [Required]
        [Display(Name="Client")]
        public int ClientID { get; set; }

        [Required]
        [Display(Name="Position")]

        public int PositionID { get; set; }

        [Required]
        [Display(Name="Contact Name")]
        public string Name { get; set; }


        public Client Client { get; set; }

        public Position Position { get; set; }
        

        public ClientContactDetail ClientContactDetail { get; set; }

       // public ICollection<ClientContact> ClientContacts { get; set; }
    }
}
