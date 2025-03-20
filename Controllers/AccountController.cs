using AutoMapper;

using BLL.AllDTOS.UserDTOS;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;

namespace KarYemek.Controllers
{
    public class AccountController : Controller
    {
       
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;

        public AccountController(UserManager<User> userManager,SignInManager<User > signInManager,IMapper mapper)
        {
            
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        // POST: /Account/Login
        [HttpPost]
       
        public async Task<IActionResult> Login(LoginDTO loginDto)
        {
            

            if (!ModelState.IsValid)
            {
                return View(loginDto);
            }

            var normalizedEmail = loginDto.Email.ToUpper();
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.NormalizedEmail == normalizedEmail);


            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Kullanıcı bulunamadı.");
                return View(loginDto);
            }

            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                ModelState.AddModelError(string.Empty, "E-posta adresi doğrulanmamış. Lütfen önce e-posta doğrulaması yapın.");
                return View(loginDto);
            }

          
            var result = await _signInManager.PasswordSignInAsync(user, loginDto.Password, false, false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "E-posta veya şifre hatalı.");
                return View(loginDto);
            }


          
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }

}
