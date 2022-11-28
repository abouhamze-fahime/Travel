using System.ComponentModel.DataAnnotations;
using Travel.Models.Travel;

namespace Travel.ViewModel
{

    public class TravelVM
    {
        public int Id { get; set; }
        public BimeGozar BimeGozar { get; set; }
        public Invoice Invoice { get; set; }
       
    }
    public class BimeGozar
    {
        [Required(ErrorMessage = "Person's name is Required*")]
        public string PersonName { get; set; }
        [Required(ErrorMessage = "Person's last name is Required*")]
        public string PersonLname { get; set; }
        public int? PersonJensId { get; set; }
        public List<PersonJens> PersonJensName { get; set; }
        public int? PersonKindId { get; set; }
        public List<PersonKind> PersonKindName { get; set; }
        [Required(ErrorMessage = "Person's BirthDate is Required*")]
        public DateTime BirthDate { get; set; }

        //public string BirthDay { get; set; }
        //public string BirthMonth { get; set; }
        //public string BirthYear { get; set; }
        //public string DayBirth { get; set; }
        //public string MonthBirth { get; set; }
        //public string YearBirth { get; set; }
        [Required(ErrorMessage = "Person's mobile is Required*")]
        public string Mobile { get; set; }
        public string Tel { get; set; }
        [Required(ErrorMessage = "Person's address is Required*")]
        [MinLength(10)]
        [MaxLength(150)]
        public string PersonAddress { get; set; }
        public string CodePosti { get; set; }
        public string FatherName { get; set; }
        public string? UnIranianCode { get; set; } 
        public string Seri { get; set; }
        public string Serial { get; set; }
        public string PassportNo { get; set; }
        public string? Nationality { get; set; }
        

    }

    public class Invoice
    {
        public int? Gid { get; set; }
        public int? GrpTakhfifId { get; set; }
        public List<GrpTakhfif> GrpTakhfifName { get; set; }
        public int? CoverLimit { get; set; }
        public List<CoverLimit> CoverLimitName { get; set; }
        public int? SafarKindId { get; set; }
        public List<SafarKind> SafarKindName { get; set; }
        public int? ModdatKindId { get; set; }
        public List<ModdatKind> ModdatKindName { get; set; }
        public int? Moddat { get; set; }
        public int? CountryId { get; set; }
        public List<Country> CountryName { get; set; }
        public int? CountryOutId { get; set; }
        public List<Country2> CountryOutName { get; set; }
        public int? LocationZoneId { get; set; }
        public List<LocationZone> LocationZoneName { get; set; }
        public int? SodurId { get; set; }
        public List<Location> SodurName { get; set; }
        public int? AgentId { get; set; }
        public List<Location>  AgentName { get; set; }
        public int? BimekindId { get; set; }
        public List<Bimekind> BimekindName { get; set; }
        public string? IssueDate { get; set; }

        public DateTime IssueDateMiladi { get; set; }

        public decimal? Amount { get; set; }

    }

    public class Location
    {
        public int LocationId { get; set; }
        public string LocationName { get; set; }

    }
    public class LocationZone
    {
        public int LocationZoneId { get; set; }
        public string LocationZoneName { get; set; }
    }
    public class Country
    {
        public int CountryId { get; set; } //short
        public string CountryName { get; set; }
    }
    public class Country2
    {
        public int CountryOutId { get; set; } //int
        public string CountryOutName { get; set; }
    }
    public class PersonKind
    {
        public int PersonKindId { get; set; }
        public string PersonKindName { get; set; }
    }
    public class ModdatKind
    {
        public int ModdatKindId { get; set; }
        public string ModdatKindName { get; set; }
    }
    public class Bimekind
    {
        public int BimekindId { get; set; }
        public string BimekindName { get; set; }
    }
    public class CoverLimit
    {
        public int CoverLimitId { get; set; }  //byte
        public string CoverLimitName { get; set; }
    }
    public class SafarKind
    {
        public int SafarKindId { get; set; }
        public string SafarKindName { get; set; }
    }
    public class PersonJens
    {
        public int PersonJensId { get; set; }
        public string PersonJensName { get; set; }
    }
    public class GrpTakhfif
    {
        public int GrpTakhfifId { get; set; }
        public string GrpTakhfifName { get; set; }
    }
    public class City
    {
        public int CityId { get; set; }
        public string CityName { get; set; }
    }
}
