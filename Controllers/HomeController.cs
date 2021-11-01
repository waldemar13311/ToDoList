using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace ToDoList.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Title = "Hello, world";
            
            return View();
        }
    }
}
