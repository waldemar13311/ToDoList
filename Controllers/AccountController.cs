using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using ToDoList.Models;
using ToDoList.Models.ViewModels;

namespace ToDoList.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userMgr, SignInManager<User> signInMgr)
        {
            _userManager = userMgr;
            _signInManager = signInMgr;
        }

        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            ViewBag.Title = "Sign in";
            ViewBag.returnUrl = returnUrl;

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(SignInUserModel dataFromUser, string returnUrl)
        {
            ViewBag.Title = "Sign in";

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(dataFromUser.Login);

                if (user != null)
                {
                    await _signInManager.SignOutAsync();

                    var result = await _signInManager.PasswordSignInAsync(
                        user,
                        dataFromUser.Password,
                        false,
                        false);

                    if (result.Succeeded)
                    {
                        return Redirect(returnUrl ?? "/");
                    }
                }

                ModelState.AddModelError("SignInErr", "Invalid user or password");
            }

            return View(dataFromUser);
        }
    }
}
