

using System.ComponentModel.DataAnnotations;

namespace Travel.ViewModel
{
    public class PrintViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Name")]
        [Required(ErrorMessage = " Please enter Person's {1} ")]
        public string PersonName { get; set; }
        [Display(Name = "Last Name")]
        public string PersonLname { get; set; }
        [Display(Name = "Birth Date")]
        public DateTime BirthDate { get; set; }
        public string Mobile { get; set; }
        [Display(Name = "Address")]
        public string PersonAddress { get; set; }
        public int?  AgentCode { get; set; }
        public string SerialNo { get; set; }

        [Display(Name = "Passport")]
        public string PassportNo { get; set; }
        [Display(Name = "Length")]
        public int? Moddat { get; set; }
        [Display(Name = "Cover Limit")]
        public string CoverLimitName { get; set; }
        [Display(Name ="Issue Date")]
        public DateTime IssueDateMiladi { get; set; }
        public decimal? Amount { get; set; }
    }
}
