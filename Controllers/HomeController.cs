using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    public class HomeController : Controller
    {
        private UserManager<User> _userManager;

        public HomeController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var user = _userManager.GetUserName(User);

            ViewBag.Title = $"Hello, {user}";


            return View();
        }

        public IActionResult Index2()
        {
            var user = _userManager.GetUserName(User);

            ViewBag.Title = $"Hello, {user}";


            return View();
        }
    }
}
