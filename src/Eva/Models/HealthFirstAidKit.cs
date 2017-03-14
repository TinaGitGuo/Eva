using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EVA.Models
{
    public class HealthFirstAidKit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int HealthID { get; set; }

        [Display(Name ="First Aid Item Required")]
        public string AidItem { get; set; }
    }
}
