using System.ComponentModel.DataAnnotations;

namespace ProjectDb.Models
{

    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Firma adı gereklidir.")]
        public string CompanyName { get; set; } // Firma adı

        [Required(ErrorMessage = "Vergi Kimlik Numarası gereklidir.")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Vergi Kimlik Numarası 10 karakter olmalıdır.")]
        public string TaxNumber { get; set; } // Vergi Kimlik Numarası

        [StringLength(11, MinimumLength = 11, ErrorMessage = "Kimlik Numarası 11 karakter olmalıdır.")]
        public string? IdentityNum { get; set; } // Kimlik Numarası



        [Required(ErrorMessage = "Telefon numarası gereklidir.")]
        [StringLength(17, MinimumLength = 17, ErrorMessage = "Uygun Formatta giriş yapınız")]
        public string Phone { get; set; }


        [Required(ErrorMessage = "E-posta adresi gereklidir.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi girin.")]
        public string Email { get; set; } // E-posta adresi

        public string? Source { get; set; } // Kaynak (Referans, reklam vb.)
        public string? FollowUpType { get; set; } // Takip tipi

        public DateTime? FollowUpApprovalDate { get; set; } // Takip onay tarihi

        public string? Language { get; set; } = "Türkçe";// Dil tercihi

        [Required(ErrorMessage = "Ülke kodu gereklidir.")]
        public string CountryCode { get; set; }
        public Country? Country { get; set; }

        [Required(ErrorMessage = "Şehir kodu gereklidir.")]
        public string CityCode { get; set; }
        public City? City { get; set; }

        public string? DistrictCode { get; set; }
        public District? District { get; set; }
        public string? AuthorizedPersonName { get; set; }
        public int AuthorizePersonId {  get; set; }
        public ICollection<AuthorizedPerson>? AuthorizedPersons { get; set; }
        public ICollection<MeetingNote>? MeetingNotes { get; set; }
    }


    public class Country
    {
        [Key]
        public string? CountryCode { get; set; } = "TR";// Ülke kodu (Primary Key)
        public string CountryDescription { get; set; } // Ülke adı
        public ICollection<City> Cities { get; set; }
    }

    public class City
    {
        [Key]
        public string CityCode { get; set; } 
        public string CityDescription { get; set; } 
        public string CountryCode { get; set; }
        public Country Country { get; set; } 

        public ICollection<District> Districts { get; set; }
    }

    public class District
    {
        [Key]
        public string DistrictCode { get; set; } 
        public string DistrictDescription { get; set; } 
        public string CityCode { get; set; }
        public City City { get; set; }

        public string CountryCode { get; set; }
        public Country Country { get; set; }
    }



    public class Sector
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } 
    }

    public class AuthorizedPerson// yetkili kişi 
    {
        [Key]
        public int Id { get; set; }
        public int? CustomerId { get; set; } = null; // Bağlı olduğu müşteri adayı ID'si
        public string Name { get; set; } // Yetkili kişi adı
        public string Phone { get; set; } // Telefon numarası
        public string Email { get; set; } // E-posta adresi
        public Customer? Customers { get; set; } = null;
    }

    public class MeetingNote//müşteri görüşme notları
    {
        [Key]
        public int Id { get; set; } 
        public required int CustomerId { get; set; }
        public DateTime? Date { get; set; } = DateTime.Now; // Görüşme tarihi 
        public int MeetingTypeId { get; set; }
        public string? Notes { get; set; } = ""; // Görüşme notları
       
        public  Guid? RowGuid { get; set; } =Guid.NewGuid();
        public string? CreatedUserName { get; set; } = "";
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        public string? LastUpdatedUserName { get; set; } = "";
        public DateTime? LastUpdatedDate { get; set; } = DateTime.Now;

        public Customer Customers { get; set; }
        public MeetingType MeetingType { get; set; }

    }

    public class MeetingType
    {
        [Key]
        public int MeetingTypeId { get; set; }

        public required string MeetingTypeDescription { get; set; }
    }


}
