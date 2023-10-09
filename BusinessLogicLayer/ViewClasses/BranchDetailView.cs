using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class BranchDetailView
    {
        [Key]
        public int BranchID { get; set; }

        [Required]
        [StringLength(50)]
        public string BranchName { get; set; }

        [Required]
        [StringLength(50)]
        public string State { get; set; }
        [Required]
        [StringLength(10)]
        public string PinCode { get; set; }
    }
}
