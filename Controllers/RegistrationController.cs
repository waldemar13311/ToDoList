using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    public class RegistrationController : Controller
    {
        private UserManager<User> userManager;

        public RegistrationController(UserManager<User> usrMngr)
        {
            userManager = usrMngr;
        }

        public IActionResult Index()
        {
            ViewBag.Title = "Registration";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserModel model)
        {
            ViewBag.Title = "Registration";
            if (ModelState.IsValid)
            {
                var newUser = new User
                {
                    UserName = model.Login,
                    Email = model.Email
                };
                
                var result = await userManager.CreateAsync(newUser, model.Password);
                
                if (result.Succeeded)
                {
                    TempData["login"] = model.Login;
                    return RedirectToAction(nameof(Success));
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                }
            }

            return View("Index");
        }

        public IActionResult Success()
        {
            ViewBag.Title = "Success";
            
            if (TempData["login"] != null)
            {
                var login = TempData["login"] as string;
            
                return View(nameof(Success), login);
;            }
            else
            {
                return null;
            }
        }
    }
}
