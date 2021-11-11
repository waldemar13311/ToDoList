using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace ToDoList.Models
{
    public class ToDoItemMsSqlProvider : IToDoItemProvider
    {
        private readonly UserDbContext _context;
        private static string _userId;

        public ToDoItemMsSqlProvider(UserDbContext ctx)
        {
            _context = ctx;
        }

        public void SetUserId(string userId)
        {
            _userId = userId;
        }

        public void AddToDoItem(ToDoItem newItem)
        {
            newItem.UserId = _userId;

            _context.ToDoItems.Add(newItem);
            _context.SaveChanges();
        }

        public void EditToDoItemMessage(long id, string newMessage)
        {
            var toDoItem = _context.ToDoItems.Find(id);
            toDoItem.Item = newMessage;
            _context.SaveChanges();
        }

        public void ChangeStatus(long id)
        {
            var toDoItem = _context.ToDoItems.Find(id);
            toDoItem.IsCompleted = !toDoItem.IsCompleted;
            _context.SaveChanges();
        }

        public void DeleteDoItem(long id)
        {
            var toDoItem = _context.ToDoItems.Find(id);
            _context.ToDoItems.Remove(toDoItem);
            _context.SaveChanges();
        }

        public IEnumerable<ToDoItem> ToDoItems
        {
            get { return _context.ToDoItems.Where(item => item.UserId == _userId).ToArray(); }
        }
    }
}
