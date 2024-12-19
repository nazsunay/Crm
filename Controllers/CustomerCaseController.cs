using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectDb.Data;
using ProjectDb.Models;

namespace ProjectDb.Controllers
{
    public class CustomerCaseController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomerCaseController(ApplicationDbContext context)
        {
            _context = context;
        }

     
        public IActionResult Index()
        {
            
            var customerCases = _context.CustomerCases
                                         .Include(cc => cc.Customer) 
                                         .ToList();

            
            return View(customerCases);
        }

        // GET: CustomerCase/Update/{id}
        public IActionResult Update(string id)
        {
            // CustomerCode'ya göre CustomerCase'i bul
            var customerCase = _context.CustomerCases
                .Include(cc => cc.Customer)
                .FirstOrDefault(cc => cc.CustomerCode == id);

            if (customerCase == null)
            {
                return NotFound();
            }

            return View(customerCase);
        }


        // POST: CustomerCase/Update/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(string id, CustomerCase updatedCustomerCase)
        {
            // CustomerCode ile id'yi karşılaştırarak doğrulama yapıyoruz
            if (updatedCustomerCase.CustomerCode == null || updatedCustomerCase.CustomerCode != id)
            {
                return BadRequest();
            }

            // Veritabanından güncellenmek istenen customerCase'i al
            var customerCase = _context.CustomerCases
                .FirstOrDefault(cc => cc.CustomerCode == updatedCustomerCase.CustomerCode);

            if (customerCase == null)
            {
                return NotFound();
            }

            // Güncellenmesi gereken alanlar
            customerCase.UserCode = updatedCustomerCase.UserCode;
            customerCase.Priority = updatedCustomerCase.Priority;
            customerCase.CaseType = updatedCustomerCase.CaseType;
            customerCase.Header = updatedCustomerCase.Header;
            customerCase.Description = updatedCustomerCase.Description;
            customerCase.LastUpdatedDate = DateTime.Now;

            try
            {
                // Veritabanına kaydet
                _context.Update(customerCase);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerCaseExists(customerCase.CustomerCode))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            // Güncellenmiş müşteri durumu ile kullanıcıyı yönlendir
            return RedirectToAction(nameof(Index));
        }


        private bool CustomerCaseExists(string customerCode)
        {
            return _context.CustomerCases.Any(e => e.CustomerCode == customerCode);
        }
    }
}