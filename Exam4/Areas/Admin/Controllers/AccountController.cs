using Exam4.Models;
using Exam4.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using System.Data;

namespace Exam4.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        private UserManager<AppUser> _userManager;
        private SignInManager<AppUser> _signInManager;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
       
        public async Task<IActionResult> Register(RegisterVm register)
        {
            if (!ModelState.IsValid) return View(register);

            AppUser appUser = new AppUser()
            {
                Email = register.Email,
                UserName = register.UserName,
               Name = register.Name,
               Surname=register.Surname

            };

            IdentityResult identityResult = await _userManager.CreateAsync(appUser, register.Password);

            if (!identityResult.Succeeded)
            {
                foreach (var error in identityResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(register);
            }
            //await _userManager.AddToRoleAsync(appUser, nameof(Roles.Member));
            await _signInManager.SignInAsync(appUser, true);


            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index","Home");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVm login)
        {
            if (!ModelState.IsValid) return View();
            AppUser user = await _userManager.FindByEmailAsync(login.UserNameOrEmail) ?? await _userManager.FindByNameAsync(login.UserNameOrEmail);
            if (user is null)
            {
                ModelState.AddModelError("", "Email or password incorrect");
                return View();
            }

            var signInResult = await _signInManager.PasswordSignInAsync(user, login.Password, false, false);
            if (!signInResult.Succeeded)
            {

                ModelState.AddModelError("", "Email or password incorrect");
                return View();
            }
            await _signInManager.SignInAsync(user, false);

           

            return RedirectToAction("Index","Home");
        }
    }
}
