using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectDb.Models;

namespace ProjectDb.ViewModels
{
  

    public class CustomerAuthorizedPerson
    {
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public int AuthorizedPersonId { get; set; }
        public AuthorizedPerson AuthorizedPerson { get; set; }
    }

    public class CustomerUpdateViewModel
    {
        public Customer Customer { get; set; }
        public List<Country> Countries { get; set; }
        public List<City> Cities { get; set; }
        public List<District> Districts { get; set; }
    }
    public class CustomerMeetCreateViewModel
    {
        public int CustomerId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Notes { get; set; }
        public int MeetingTypeId { get; set; }
        public string CreatedUserName { get; set; }

        public Customer Customer { get; set; }
        public List<SelectListItem> Countries { get; set; }
        public List<SelectListItem> CompanyNames { get; set; }
        public List<SelectListItem> MeetingTypes { get; set; }
    }
    public class CustomerMeetUpdateViewModel
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        public string? Notes { get; set; }
        public int MeetingTypeId { get; set; }
        public string LastUpdatedUserName { get; set; }

        public List<SelectListItem> CompanyNames { get; set; }
        public List<SelectListItem> MeetingTypes { get; set; }
    }

    public class CustomerCreateViewModel
    {
        public Customer Customer { get; set; }
        public AuthorizedPerson AuthorizedPerson { get; set; }
        public List<SelectListItem> Countries { get; set; }
        public List<SelectListItem> Cities { get; set; }
        public List<SelectListItem> Districts { get; set; }

        public List<SelectListItem> AuthorizedPersons { get; set; }
        public List<int> SelectedAuthorizedPersonIds { get; set; }
        public int AuthorizePersonId { get; set; }
        public string AuthorizePersonName { get; set; }


        public CustomerCreateViewModel()
        {
            Countries = new List<SelectListItem>();
            Cities = new List<SelectListItem>();
            Districts = new List<SelectListItem>();
            AuthorizedPersons = new List<SelectListItem>();

        }
    }
  


}
