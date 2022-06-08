using KUSYS_Demo.Business.DTOs;
using KUSYS_Demo.Entity.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KUSYS_Demo.Controllers
{
    public class LoginController : Controller
    {

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public LoginController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

      

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto userLoginDto)
        {
            if (ModelState.IsValid)
            {

                var users = await _userManager.Users.ToListAsync();
                var user = await _userManager.FindByNameAsync(userLoginDto.Username);
                if (user != null)
                {
                    //var result = await _signInManager.PasswordSignInAsync(user, userLoginDto.Password, false, false);
                    var result = await _signInManager.PasswordSignInAsync(userLoginDto.Username, userLoginDto.Password, false, false);

                    if (result.Succeeded)
                    {
                       
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "E-posta adresiniz veya şifreniz yanlıştır.");
                        return View("Index");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "E-posta adresiniz veya şifreniz yanlıştır.");
                    return View("Index");
                }
            }
            else
            {
                return View("Index");
            }

        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Login", new { Area = "" });
        }

    }
}
