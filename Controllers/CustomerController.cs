using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectDb.Models;
using System;
using ProjectDb.Data;
using ProjectDb.ViewModels;

namespace ProjectDb.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomerController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string searchTerm)
        {
            var customersQuery = _context.Customers.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                customersQuery = customersQuery.Where(c => c.CompanyName.Contains(searchTerm));
                ViewData["SearchTerm"] = searchTerm;
            }

            
            var customerList = (from c in customersQuery
                                join ap in _context.AuthorizedPersons on c.AuthorizePersonId equals ap.Id into authorizedPersons
                                from ap in authorizedPersons.DefaultIfEmpty()  
                                select new Customer
                                {
                                    Id = c.Id,
                                    CompanyName = c.CompanyName,
                                    TaxNumber = c.TaxNumber,
                                    IdentityNum = c.IdentityNum,
                                    Phone = c.Phone,
                                    Email = c.Email,
                                    Source = c.Source,
                                    FollowUpType = c.FollowUpType,
                                    FollowUpApprovalDate = c.FollowUpApprovalDate,
                                    AuthorizedPersonName = ap != null ? ap.Name : "Yetkili Kişi Bulunamadı"  
                                }).ToList();

            return View(customerList);  
        }


        public IActionResult CustomerCreate()
        {
            var model = new CustomerCreateViewModel
            {
                Customer = new Customer(), 
                Countries = _context.Countries.Select(c => new SelectListItem
                {
                    Value = c.CountryCode,
                    Text = c.CountryDescription
                }).ToList(),
                Cities = new List<SelectListItem>(),
                Districts = new List<SelectListItem>(),
                AuthorizedPersons = _context.AuthorizedPersons.Select(ap => new SelectListItem
                {
                    Value = ap.Id.ToString(),
                    Text = ap.Name
                }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult CustomerCreate(CustomerCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
               
                if (model.Customer.AuthorizePersonId > 0)
                {
                    var selectedPerson = _context.AuthorizedPersons
                                                  .FirstOrDefault(ap => ap.Id == model.Customer.AuthorizePersonId);
                    if (selectedPerson != null)
                    {
                        model.Customer.AuthorizedPersonName = selectedPerson.Name;
                    }
                }

                
                _context.Customers.Add(model.Customer);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

           
            model.Countries = _context.Countries.Select(c => new SelectListItem
            {
                Value = c.CountryCode,
                Text = c.CountryDescription
            }).ToList();

            model.Cities = !string.IsNullOrEmpty(model.Customer.CountryCode)
                ? _context.Cities
                    .Where(c => c.CountryCode == model.Customer.CountryCode)
                    .Select(c => new SelectListItem
                    {
                        Value = c.CityCode,
                        Text = c.CityDescription
                    }).ToList()
                : new List<SelectListItem>();

            model.Districts = !string.IsNullOrEmpty(model.Customer.CityCode)
                ? _context.Districts
                    .Where(d => d.CityCode == model.Customer.CityCode)
                    .Select(d => new SelectListItem
                    {
                        Value = d.DistrictCode,
                        Text = d.DistrictDescription
                    }).ToList()
                : new List<SelectListItem>();

            
            model.AuthorizedPersons = _context.AuthorizedPersons.Select(ap => new SelectListItem
            {
                Value = ap.Id.ToString(),
                Text = ap.Name
            }).ToList();

            return View(model);
        }



        [HttpGet]
        public IActionResult GetCities(string countryCode)
        {
            if (string.IsNullOrEmpty(countryCode))
            {
                return BadRequest("Ülke kodu gerekli.");
            }

            var cities = _context.Cities
                                 .Where(c => c.CountryCode == countryCode)
                                 .Select(c => new { c.CityCode, c.CityDescription })
                                 .ToList();

            return Json(cities);
        }

        
        [HttpGet]
        public IActionResult GetDistricts(string cityCode)
        {
            if (string.IsNullOrEmpty(cityCode))
            {
                return BadRequest("Şehir kodu gerekli.");
            }

            var districts = _context.Districts
                                    .Where(d => d.CityCode == cityCode)
                                    .Select(d => new { d.DistrictCode, d.DistrictDescription })
                                    .ToList();

            return Json(districts);
        }


        public IActionResult AuthorizedPersonCreate()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AuthorizedPersonCreate(AuthorizedPerson model)
        {
            if (ModelState.IsValid)
            {

                _context.AuthorizedPersons.Add(model);
                _context.SaveChanges();


               


                return RedirectToAction("CustomerCreate");
            }


            return View(model);
        }


        public IActionResult CustomerMeetList(string searchTerm)
        {
            var meetingNotes = _context.MeetingNotes
                .Include(m => m.Customers)
                .Include(m => m.MeetingType)
                .AsQueryable();


            if (!string.IsNullOrEmpty(searchTerm))
            {
                meetingNotes = meetingNotes.Where(m =>
                    m.Customers.CompanyName.Contains(searchTerm) ||
                    m.Notes.Contains(searchTerm) ||
                    m.MeetingType.MeetingTypeDescription.Contains(searchTerm));

                ViewData["SearchTerm"] = searchTerm;
            }


            var meetingNotesList = meetingNotes
                .Select(m => new
                {
                    m.Id,
                    m.Customers.CompanyName,
                    m.MeetingType.MeetingTypeDescription,
                    m.Date,
                    m.Notes,
                    LastUpdatedDate = m.LastUpdatedDate ?? m.CreatedDate,
                    LastUpdatedUserName = string.IsNullOrEmpty(m.LastUpdatedUserName) ? m.CreatedUserName : m.LastUpdatedUserName
                }).ToList();

            return View(meetingNotesList);
        }


        [HttpGet]
        public IActionResult CustomerMeetCreate()
        {
            var companyNames = _context.Customers
                .OrderBy(c => c.CompanyName)
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.CompanyName
                }).ToList();

            var meetingTypes = _context.MeetingTypes
                .OrderBy(mt => mt.MeetingTypeDescription)
                .Select(mt => new SelectListItem
                {
                    Value = mt.MeetingTypeId.ToString(),
                    Text = mt.MeetingTypeDescription
                }).ToList();

            var model = new CustomerMeetCreateViewModel
            {
                CompanyNames = companyNames,
                MeetingTypes = meetingTypes
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CustomerMeetCreate(CustomerMeetCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var customer = _context.Customers
                    .FirstOrDefault(c => c.Id == model.CustomerId);

                if (customer != null)
                {
                    var existingNotes = _context.MeetingNotes
                        .Where(m => m.CustomerId == model.CustomerId)
                        .ToList();

                    if (existingNotes.Count >= 3)
                    {
                        ModelState.AddModelError("", "Bu kullanıcı için zaten 3 tane görüşme notu var.");
                        return View(model);
                    }

                    var newMeetingNote = new MeetingNote
                    {
                        CustomerId = customer.Id,
                        CreatedDate = model.CreatedDate = DateTime.Now,
                        Notes = model.Notes,
                        MeetingTypeId = model.MeetingTypeId,
                        RowGuid = Guid.NewGuid(),
                        CreatedUserName = model.CreatedUserName,

                    };

                    _context.Add(newMeetingNote);
                    await _context.SaveChangesAsync();

                    return RedirectToAction("CustomerMeetList", "Customer", new { id = newMeetingNote.CustomerId });
                }

                ModelState.AddModelError("", "Müşteri bulunamadı.");
            }


            return View(model);
        }


        [HttpGet]
        public IActionResult CustomerUpdate(int id)
        {
            var customer = _context.Customers
                .Include(c => c.Country)
                .Include(c => c.City)
                .Include(c => c.District)
                .Include(c => c.AuthorizedPersons)
                .FirstOrDefault(c => c.Id == id);

            if (customer == null)
            {
                return NotFound();
            }

            var model = new CustomerUpdateViewModel
            {
                Customer = customer,
                Countries = _context.Countries.Where(c => c.CountryCode == customer.CountryCode).ToList(),
                Cities = _context.Cities.Where(c => c.CityCode == customer.CityCode).ToList(),
                Districts = _context.Districts.Where(d => d.DistrictCode == customer.DistrictCode).ToList()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CustomerUpdate(int id, CustomerUpdateViewModel model)
        {

            var customer = await _context.Customers

                .FirstOrDefaultAsync(c => c.Id == id);

            if (customer == null)
            {
                return NotFound();
            }


            customer.CompanyName = model.Customer.CompanyName;
            customer.TaxNumber = model.Customer.TaxNumber;
            customer.IdentityNum = model.Customer.IdentityNum;
            customer.Phone = model.Customer.Phone;
            customer.Email = model.Customer.Email;
            customer.Source = model.Customer.Source;
            customer.FollowUpType = model.Customer.FollowUpType;
            customer.FollowUpApprovalDate = model.Customer.FollowUpApprovalDate;
            customer.CountryCode = model.Customer.CountryCode;
            customer.CityCode = model.Customer.CityCode;
            customer.DistrictCode = model.Customer.DistrictCode;



            _context.Update(customer);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        public IActionResult CustomerMeetUpdate(int id)
        {

            var meetingNote = _context.MeetingNotes
                                      .Include(m => m.Customers)
                                      .Include(m => m.MeetingType)
                                      .FirstOrDefault(m => m.Id == id);

            if (meetingNote == null)
            {
                return NotFound();
            }


            var viewModel = new CustomerMeetUpdateViewModel
            {
                Id = meetingNote.Id,
                CustomerId = meetingNote.CustomerId,
                LastUpdatedDate = meetingNote.LastUpdatedDate,
                Notes = meetingNote.Notes,
                MeetingTypeId = meetingNote.MeetingTypeId,
                LastUpdatedUserName = meetingNote.LastUpdatedUserName,


                CompanyNames = _context.Customers
                                       .Select(c => new SelectListItem
                                       {
                                           Value = c.Id.ToString(),
                                           Text = c.CompanyName
                                       }).ToList(),


                MeetingTypes = _context.MeetingTypes
                                       .Select(mt => new SelectListItem
                                       {
                                           Value = mt.MeetingTypeId.ToString(),
                                           Text = mt.MeetingTypeDescription
                                       }).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]

        public IActionResult CustomerMeetUpdate(int id, CustomerMeetUpdateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var meetingNote = _context.MeetingNotes
                                          .FirstOrDefault(m => m.Id == id);

                if (meetingNote == null)
                {
                    return NotFound();
                }


                meetingNote.CustomerId = model.CustomerId;
                meetingNote.LastUpdatedDate = DateTime.Now;
                meetingNote.Notes = model.Notes;
                meetingNote.MeetingTypeId = model.MeetingTypeId;
                meetingNote.LastUpdatedUserName = model.LastUpdatedUserName;

                _context.Update(meetingNote);
                _context.SaveChanges();

                TempData["SuccessMessage"] = "Görüşme başarıyla güncellendi!";
                return RedirectToAction("CustomerMeetList", new { id = meetingNote.Id });
            }


            model.CompanyNames = _context.Customers
                                         .Select(c => new SelectListItem
                                         {
                                             Value = c.Id.ToString(),
                                             Text = c.CompanyName
                                         }).ToList();

            model.MeetingTypes = _context.MeetingTypes
                                         .Select(mt => new SelectListItem
                                         {
                                             Value = mt.MeetingTypeId.ToString(),
                                             Text = mt.MeetingTypeDescription
                                         }).ToList();

            return View(model);
        }

        [HttpPost, ActionName("CustomerMeetDelete")]
        public async Task<IActionResult> CustomerMeetDeleteConfirmed(int id)
        {
            var meetingNote = await _context.MeetingNotes.FindAsync(id);
            if (meetingNote == null)
            {
                return NotFound();
            }

            _context.MeetingNotes.Remove(meetingNote);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(CustomerMeetList)); 
        }

        public IActionResult CustomerMeetDetail(int id)
        {
            
            var meetingNote = _context.MeetingNotes
                .Include(m => m.Customers)
                .Include(m => m.MeetingType)
                .FirstOrDefault(m => m.Id == id);

            if (meetingNote == null)
            {
                return NotFound(); 
            }

            return View(meetingNote); 
        }
        public IActionResult CustomerMeetTypCreate()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CustomerMeetTypCreate(MeetingType meetingType)
        {
            if (ModelState.IsValid)
            {
                
                _context.MeetingTypes.Add(meetingType);
                _context.SaveChanges();

                return RedirectToAction("CustomerMeetCreate"); 
            }

           
            return View(meetingType);
        }



    }


}