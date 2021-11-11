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
        private SignInManager<User> _signInManager;
        private static IToDoItemProvider _toDoItemProvider;
        private UserDbContext context;

        private ProviderFactory providerFactory;

        private static ListState listState = ListState.All;

        public HomeController(
            UserManager<User> userManager, 
            SignInManager<User> signInManager,
            ProviderFactory providerFactory, UserDbContext context
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            this.providerFactory = providerFactory;
            this.context = context;
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

                // Тут должно быть получение ToDoItemMsSqlProvider
                _toDoItemProvider = providerFactory.GetProvider("mssql");
                ((ToDoItemMsSqlProvider)_toDoItemProvider).SetUserId(_userManager.GetUserId(User));

                // TODO ПРПРОБОВАТЬ СДЕЛАТЬ ПОЛУЧЕНИЕ DBCONTEXT В ФАБРИКЕ
                ((ToDoItemMsSqlProvider)_toDoItemProvider).SetUserDbContext(context);
            }
            else
            {
                // Тут должно быть получение ToDoItemMemoryProvider
                _toDoItemProvider = providerFactory.GetProvider("memory");
            }

            ViewBag.EditId = TempData["EditId"];

            var model = _toDoItemProvider.ToDoItems;

            switch (listState)
            {
                case ListState.Active:
                    model = _toDoItemProvider.ToDoItems.Where(i => !i.IsCompleted);
                    break;

                case ListState.Completed:
                    model = _toDoItemProvider.ToDoItems.Where(i => i.IsCompleted);
                    break;
            }

            return View(model);
        }


        /////////////////////
        /// Возможно в этих 3-ёх методах нужно сделать return View("Index", model),
        /// чтобы в URL было видно ALL Completed и Active
        public IActionResult OnlyActiveToDoItems()
        {
            listState = ListState.Active;

            return RedirectToAction(nameof(Index));
        }

        public IActionResult OnlyCompletedToDoItems()
        {
            listState = ListState.Completed;

            return RedirectToAction(nameof(Index));
        }

        public IActionResult AllToDoItems()
        {
            listState = ListState.All;

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

            _toDoItemProvider.EditToDoItemMessage(newTodoItem.Id, newTodoItem.Item);


            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult AddToDoItem(ToDoItem newItem)
        {
            // Просто чтобы бы не пустой Item
            if (ModelState.IsValid)
            { 
                _toDoItemProvider.AddToDoItem(newItem);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult DeleteToDoItem(long id)
        {
            _toDoItemProvider.DeleteDoItem(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult ChangeToDoItemStatus(long id)
        {
            _toDoItemProvider.ChangeStatus(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
