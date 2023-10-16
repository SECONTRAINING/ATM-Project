using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ATMWebAPP.Models
{
    public class CustomerAccountView
    {
        [Key]
        public int CustomerID { get; set; }

        [Required]
        [StringLength(30)]
        public string FullName { get; set; }

        [Required]
        [StringLength(30)]
        public string FathersName { get; set; }

        [Required]
        public DateTime DOB { get; set; }

        [Required]
        [StringLength(7)]
        public string Gender { get; set; }

        [Required]
        [StringLength(100)]
        public string PermanentAddress { get; set; }

        [Required]
        [StringLength(100)]
        public string PresentAddress { get; set; }

        [StringLength(10)]
        public string PANNumber { get; set; }

        [Required]
        public string MobileNumber { get; set; }

        public string Landline { get; set; }

        [Required]
        [StringLength(10)]
        public string AccountType { get; set; }

        [Required]
        public decimal OpeningBalance { get; set; }

        public int AccNo { get; set; }

        public int CardNo { get; set; }

        [Required]
        [StringLength(4)]
        public string PIN { get; set; }

        public string CheckBook { get; set; }

        [Required]
        public int BranchID { get; set; }
    }
}
