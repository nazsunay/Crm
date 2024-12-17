using Microsoft.AspNetCore.Mvc;
using ProjectDb.Data;
using ProjectDb.Models;

namespace ProjectDb.Controllers
{
    public class loginController : Controller
    {
        private readonly ApplicationDbContext _context;

        public loginController(ApplicationDbContext context)
        {
            _context = context;
        }

       
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Login(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("", "Kullanıcı adı veya şifre boş olamaz.");
                return View();
            }

            // Girilen bilgileri Login model örneğine atıyoruz.
            var loginModel = new Login
            {
                UserName = userName,
                Password = password
            };

            var user = _context.Logins
                .FirstOrDefault(u => u.UserName == loginModel.UserName && u.Password == loginModel.Password);

            if (user != null)
            {
                // Kullanıcı bilgilerini session'a kaydediyoruz
                HttpContext.Session.SetString("UserName", user.UserName);
                HttpContext.Session.SetInt32("UserId", user.Id);

                // Kullanıcıyı Index sayfasına yönlendiriyoruz
                return RedirectToAction("Index", "Home", new {userName = user.UserName});
            }
            else
            {
                ModelState.AddModelError("", "Geçersiz kullanıcı adı veya şifre.");
                // Hata durumunda bile girilen bilgileri view'a geri gönderebilirsiniz.
                return View(loginModel);
            }
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();  
            return RedirectToAction("Login"); 
        }
    }
}
