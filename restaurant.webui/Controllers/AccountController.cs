using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using restaurant.business.Abstract;
using restaurant.webui.Identity;
using restaurant.webui.Models;

namespace restaurant.webui.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<User> _userManager;
        private SignInManager<User> _signManager;
        private ICartService _cartService;

        public AccountController(UserManager<User> userManager, SignInManager<User> signManager, ICartService cartService)
        {
            _userManager = userManager;
            _signManager = signManager;
            _cartService = cartService;
        }
        public IActionResult Login(string? ReturnUrl)
        {
            return View(new LoginModel
            {
                ReturnUrl = ReturnUrl
            });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.UserName);
                if (user == null)
                {
                    ModelState.AddModelError("", "Username or password is incorrect");
                    return View(model);
                }
                var result = await _signManager.PasswordSignInAsync(user, model.Password, false, false);
                if (result.Succeeded)
                {
                    return Redirect(model.ReturnUrl ?? "~/");
                }
                ModelState.AddModelError("", "Username or password is incorrect");
                return View(model);
            }
            return View(model);

        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = new User()
            {
                FirstName = model.FirstName,
                Email = model.Email,
                UserName = model.UserName
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                _cartService.InitializeCart(user.Id);
                return RedirectToAction("Login", "Account");
            }
            foreach (var message in result.Errors)
            {
                ModelState.AddModelError("",message.Description);
            }
            return View(model);
        }

        public async Task<IActionResult> LogOut()
        {
            await _signManager.SignOutAsync();
            return Redirect("~/");
        }
    }
}