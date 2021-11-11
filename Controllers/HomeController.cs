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
    public class HomeController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ProviderFactory _providerFactory;

        private static ListState _listState = ListState.All;
        private static string _typeOfModeDataSaver = "memory";

        public HomeController(
            UserManager<User> userManager, 
            SignInManager<User> signInManager,
            ProviderFactory providerFactory
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;

            _providerFactory = providerFactory;
        }

        public IActionResult LogOut()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }

        public IActionResult Index()
        {
            var user = _userManager.GetUserName(User);

            if (user != null)
            {
                ViewBag.UserName = user;
                _typeOfModeDataSaver = "mssql";
                ((ToDoItemMsSqlProvider)(_providerFactory.GetProvider(_typeOfModeDataSaver))).SetUserId(_userManager.GetUserId(User));
            }
            else
            {
                _typeOfModeDataSaver = "memory";
            }

            ViewBag.EditId = TempData["EditId"];

            var model = _providerFactory.GetProvider(_typeOfModeDataSaver).ToDoItems;

            switch (_listState)
            {
                case ListState.Active:
                    model = _providerFactory.GetProvider(_typeOfModeDataSaver).ToDoItems.Where(i => !i.IsCompleted);
                    break;

                case ListState.Completed:
                    model = _providerFactory.GetProvider(_typeOfModeDataSaver).ToDoItems.Where(i => i.IsCompleted);
                    break;
            }

            return View(model);
        }


        /////////////////////
        /// Возможно в этих 3-ёх методах нужно сделать return View("Index", model),
        /// чтобы в URL было видно ALL Completed и Active
        public IActionResult OnlyActiveToDoItems()
        {
            _listState = ListState.Active;

            return RedirectToAction(nameof(Index));
        }

        public IActionResult OnlyCompletedToDoItems()
        {
            _listState = ListState.Completed;

            return RedirectToAction(nameof(Index));
        }

        public IActionResult AllToDoItems()
        {
            _listState = ListState.All;

            return RedirectToAction(nameof(Index));
        }
        /////////////////////
        
        public IActionResult EditToDoItem(long id)
        {
            TempData["EditId"] = Convert.ToInt32(id);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult EditToDoItem(ToDoItem newTodoItem)
        {
            var referer = Request.Headers["Referer"];

            _providerFactory.GetProvider(_typeOfModeDataSaver).EditToDoItemMessage(newTodoItem.Id, newTodoItem.Item);


            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult AddToDoItem(ToDoItem newItem)
        {
            // Просто чтобы бы не пустой Item
            if (ModelState.IsValid)
            {
                _providerFactory.GetProvider(_typeOfModeDataSaver).AddToDoItem(newItem);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult DeleteToDoItem(long id)
        {
            _providerFactory.GetProvider(_typeOfModeDataSaver).DeleteDoItem(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult ChangeToDoItemStatus(long id)
        {
            _providerFactory.GetProvider(_typeOfModeDataSaver).ChangeStatus(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
